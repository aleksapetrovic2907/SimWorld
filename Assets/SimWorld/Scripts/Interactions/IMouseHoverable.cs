namespace AP
{
    public interface IMouseHoverable
    {
        public void OnMouseEntered(bool isInRange);
        public void OnMouseStay(bool isInRange);
        public void OnMouseExited();
    }
}