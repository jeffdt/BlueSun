using Microsoft.Xna.Framework.Input;
using pigeon.gameobject;
using pigeon.input;
using pigeon.sound;
using System.Collections.Generic;
using System.Linq;

namespace BlueSun.src.worlds {
    internal class SfxController : Component {
        private readonly Dictionary<Keys, string> sfxKeys = new Dictionary<Keys, string> {
            { Keys.D1, "sfx0" },
            { Keys.D2, "sfx1" },
            { Keys.D3, "sfx2" },
            { Keys.D4, "sfx3" },
            { Keys.D5, "sfx4" },
            { Keys.D6, "sfx5" },
            { Keys.D7, "sfx6" },
            { Keys.D8, "sfx7" },
            { Keys.D9, "sfx8" },
            { Keys.D0, "sfx9" }
        };

        protected override void Initialize() { }

        protected override void Update() {
            foreach (var key in sfxKeys.Keys.Where(RawKeyboardInput.IsPressed).Select(key => key)) {
                Sfx.PlaySfx(sfxKeys[key]);
            }
        }
    }
}