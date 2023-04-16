namespace BigBlue
{
    public class Property
    {
        public bool isYou;
        public bool isPush;
        public bool isStop;
        public bool isKill;
        public bool isWin;
        public bool isText;

        public Property()
        {
            isYou = false;
            isPush = false;
            isStop = false;
            isKill = false;
            isWin = false;
        }
        
        public Property(bool isYou = false, bool isPush = false, bool isStop = false, bool isKill = false, bool isWin = false)
        {
            this.isYou = isYou;
            this.isPush = isPush;
            this.isStop = isStop;
            this.isKill = isKill;
            this.isWin = isWin;
        }

        public Property Clone()
        {
            return new Property(isYou, isPush, isStop, isKill, isWin);
        }
    }
}
