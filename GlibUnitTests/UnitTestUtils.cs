using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glib.XNA;
using Glib.XNA.SpriteLib;

namespace GlibUnitTests
{
    class UnitTestSprite : ISprite
    {
        public void Draw()
        {
            IsDrawn = true;
        }

        public bool IsDrawn = false;
        public bool IsUpdated = false;

        public void Update()
        {
            IsUpdated = true;
        }
    }

    class UnitTestUpdater : Updater
    {
        public UnitTestUpdater(SpriteWrapper sw)
            : base(sw)
        {
            _id = Guid.NewGuid();
        }

        public Guid UpdaterID
        {
            get
            {
                return _id;
            }
        }

        private Guid _id;

        public int UpdatedSprites = 0;

        public IEnumerable<ISprite> AllSprites
        {
            get
            {
                return this.GetAllSprites();
            }
        }

        public override void UpdateSprite(ISprite updating)
        {
            UpdatedSprites++;
        }
    }
}
