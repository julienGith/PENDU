using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrouveLeMot;

namespace TrouveLeMot
{
    public class Niveau
    {
        private bool _perso;
        private bool _facile;
        private bool _difficile;
        private bool _expert;

        public bool Perso
        {
            get { return (this._perso); }
            set { }
        }
        public bool Facile
        {
            get { return (this._facile); }
            set { }
        }
        public bool Difficile 
        {
            get { return (this._difficile); }
            set { }
        }
        public bool Expert
        {
            get { return (this._expert); }
            set { }
        }
        public Niveau (){ }
    }
}
