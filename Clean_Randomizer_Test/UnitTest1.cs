using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clean_Randomizer.Util;

namespace Clean_Randomizer_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void readImage()
        {
            ImageData imageData = ImageLoader.LoadImage("0_4");
        }
    }
}
