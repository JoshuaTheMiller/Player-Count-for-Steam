using System;

namespace Trfc.ClientFramework
{
    //https://docs.microsoft.com/en-us/xamarin/ios/deploy-test/linker?tabs=vsmac
    public sealed class PreserveAttribute : Attribute
    {
        public bool AllMembers = true;
        public bool Conditional = false;
    }
}
