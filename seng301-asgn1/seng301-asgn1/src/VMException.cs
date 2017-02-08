using System;

namespace seng301_asgn1
{
    public class VMException:Exception
    {
        public VMException()
        {
        }

        public VMException(string message)
            : base(message)
        {
        }

    }
}
