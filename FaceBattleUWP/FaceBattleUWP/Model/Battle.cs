using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceBattleUWP.Model
{
    public class Battle : ViewModelBase
    {
        private int _bid;
        public int BID
        {
            get
            {
                return _bid;
            }
            set
            {
                if (_bid != value)
                {
                    _bid = value;
                    RaisePropertyChanged(() => BID);
                }
            }
        }

        private int _type;
        public int Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    RaisePropertyChanged(() => Type);
                }
            }
        }



        public Battle()
        {

        }
    }
}
