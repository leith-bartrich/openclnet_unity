openclnet_unity
===============

a slightly hacked up version of OpenCL.Net for use in Unity3D

Please check the license.txt files in each subdirectory for licensing details for that component.

You may direct questions or concerns to FIE LLC:
http://www.fie.us

unityopenclnet is a project to standardize and maintain a set of
bindings for OpenCL for use in Unity3D.

Firstly, it repackages an existing open source project, OpenCL.Net.  It removes all the heavier visual studio and built target cruft.
http://openclnet.codeplex.com
It also repackages some dependencies from extraconstraints.
https://github.com/Fody/ExtraConstraints

Second, it makes very minor modifications to OpenCL.Net's source in order
to allow it to compile in Unity3D's version of mono.

Third, it makes minor modifications to how OpenCL.Net links to the
platform's native OpenCL such that it will work with as many
Unity editing and playing platforms as possible.  Ideally, over
time, the supported platforms will expand.  Keeping up to date with this
and testing on all platforms, is something I would hope the community
could assist with and contribute back.

Fourth, it adds some convenience functions where they are obvious.

Tools that make OpenCL useful and accessible to Unity3D can be built atop
this layer.  The intention is for the community to maintain this common,
low-level OpenCL layer.