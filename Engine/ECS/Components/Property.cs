namespace BigBlue
{
    public class Property
    {
        public bool isYou;
        public bool isPush;
        public bool isStop;
        public bool isKill;
        public bool isWin;

        public Property(bool isYou, bool isPush, bool isStop, bool isKill, bool isWin)
        {
            this.isYou = isYou;
            this.isPush = isPush;
            this.isStop = isStop;
            this.isKill = isKill;
            this.isWin = isWin;
        }

        public Property(bool isPush)
        {
            isYou = false;
            this.isPush = isPush;
            isStop = false;
            isKill = false;
            isWin = false;
        }

        public Property()
        {
            isYou = false;
            isPush = false;
            isStop = false;
            isKill = false;
            isWin = false;
        }
    }
}
