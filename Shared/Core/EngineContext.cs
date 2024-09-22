using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core
{
    public class EngineContext
    {
        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Create();
                }

                return Singleton<IEngine>.Instance;
            }
        }

        public static IEngine Create()
        {
            return Singleton<IEngine>.Instance ?? (Singleton<IEngine>.Instance = new Engine());
        }
    }
}
