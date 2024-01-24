#!/bin/bash

version=""

for line in `cat ./build/version`
do
    version=$line
done

# $1 is Linux-x64
# $2 is amd64

mkdir ./build_out

build_deb() {

    echo "ColorMC build $1 version: $version"

    base=./src/build_out/$1-dotnet
    base_dir="$base/colormc"
    deb_name="./build_out/colormc-a$version-$1.deb"

    dotnet publish ./src/ColorMC.Launcher -p:PublishProfile=$1

    mkdir $base_dir

    pdbs=("ColorMC.Gui.pdb" "ColorMC.Core.pdb" "Live2DCSharpSDK.App.pdb"
        "Live2DCSharpSDK.Framework.pdb" "ColorMC.Launcher.pdb" "ColorMC.Launcher")

    cp -r ./build/info/linux/* $base_dir
    cp -r ./build/info/$1/* $base_dir

    sed -i "s/%version%/$version/g" $base_dir/DEBIAN/control

    dir=usr/share/ColorMC

    mkdir $base_dir/$dir

    for line in ${pdbs[@]}
    do
        cp $base/$line \
            $base_dir/$dir/$line
    done

    chmod -R 775 $base_dir/DEBIAN/postinst

    dpkg -b $base_dir $deb_name

    echo "ColorMC $1 build done"
}

# build_deb linux-x64
# build_deb linux-arm64

echo "ColorMC build linux-x64-appimage version: $version"

build_run=./build_run

mkdir $build_run

if [ ! -f "$build_run/deb2appimage.AppImage" ];then
    wget https://github.com/simoniz0r/deb2appimage/releases/download/v0.0.5/deb2appimage-0.0.5-x86_64.AppImage
    mv ./deb2appimage-0.0.5-x86_64.AppImage $build_run/deb2appimage.AppImage
fi

cp ./build/info/appimg.json $build_run/appimg.json

arch=x64

sed -i "s/%version%/$version/g" $build_run/appimg.json
sed -i "s/%arch%/$arch/g" $build_run/appimg.json

chmod a+x $build_run/deb2appimage.AppImage

sudo apt-get install libfuse2 curl -y

sudo $build_run/deb2appimage.AppImage -j $build_run/appimg.json -o ./build_out

sudo chown : ./build_out/colormc-a$version-x86_64.AppImage
chmod a+x build_out/colormc-a$version-x86_64.AppImage

echo "ColorMC linux-appimage build done"