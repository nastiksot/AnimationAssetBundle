namespace Models
{
    public class AnimationPaths
    {
        private string standingIdleAnimationPath;
        private string runForwardAnimationPath;
         
        public string StandingIdleAnimationPath
        {
            get => standingIdleAnimationPath;
            set => standingIdleAnimationPath = value;
        }

        public string RunForwardAnimationPath
        {
            get => runForwardAnimationPath;
            set => runForwardAnimationPath = value;
        }
    }
}