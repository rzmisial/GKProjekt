using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Turret
{
    /// <summary>
    /// Interface used for turrets that are supposed to be possible to upgrade.
    /// </summary>
    public interface IUpgrading
    {
        /// <summary>
        /// Performs turret upgrade.
        /// </summary>
        void Upgrade();
    }
}
