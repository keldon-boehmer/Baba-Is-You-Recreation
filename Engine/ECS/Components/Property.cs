using Baba.ECS;
using Engine.ECS;

namespace Baba
{
    public class Property
    {
        public bool isYou;
        public bool isPush;
        public bool isStop;
        public bool isKill;
        public bool isWin;
        public bool isSink;

        public bool isText;

        public Property()
        {
            isYou = false;
            isPush = false;
            isStop = false;
            isKill = false;
            isWin = false;
            isSink = false;
        }
        
        public Property(bool isYou = false, bool isPush = false, bool isStop = false, bool isKill = false, bool isWin = false, bool isSink = false)
        {
            this.isYou = isYou;
            this.isPush = isPush;
            this.isStop = isStop;
            this.isKill = isKill;
            this.isWin = isWin;
            this.isSink = isSink;
        }
        internal void Apply(ActionType actionType)
        {
            switch (actionType)
            {
                case (ActionType.You) : isYou = true; break;
                case (ActionType.Push): isPush = true; break;
                case (ActionType.Stop): isStop = true; break;
                case (ActionType.Kill): isKill = true; break;
                case (ActionType.Win) : isWin = true; break;
                case (ActionType.Sink): isSink = true; break;
            }
        }

        internal void Clear()
        {
            isYou = false;
            isPush = false;
            isStop = false;
            isKill = false;
            isWin = false;
            isSink = false;
        }

        public bool Equals(Property other)
        {
            if (other.isYou != this.isYou)
                return false;
            if (other.isPush != this.isPush)
                return false;
            if (other.isStop != this.isStop)
                return false;
            if (other.isKill != this.isKill)
                return false;
            if (other.isWin != this.isWin)
                return false;
            if (other.isSink != this.isSink)
                return false;
            return true;
        }

        public Property Clone()
        {
            return new Property(isYou, isPush, isStop, isKill, isWin, isSink);
        }
    }
}
