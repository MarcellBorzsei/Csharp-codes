using Kitolas.Model;
using Kitolas.Persistence;
using Moq;
using System.Xml;
using System.Drawing;

namespace KitolasTest
{
    [TestClass]
    public class KitolasTest
    {
        private Game _model = null!;
        private SlideTable _mockedTable = null!;
        private Mock<IKitolasDataAccess> _mock = null!;

        [TestInitialize]
        public void Initialize()
        {
            List<Point> fixedBlackstones = new List<Point> { new Point(0, 1), new Point(0, 2), new Point(1, 0) };
            List<Point> fixedWhitestones = new List<Point> { new Point(0, 0), new Point(2, 0), new Point(2, 2) };
            _mockedTable = new SlideTable(3, fixedBlackstones, fixedWhitestones);



            _mock = new Mock<IKitolasDataAccess>();
            _mock.Setup(mock => mock.LoadAsync(It.IsAny<String>()))
                .Returns(() => Task.FromResult(_mockedTable));

            _model = new Game(_mockedTable, _mock.Object);

        }

        [TestMethod]

        public void TestConstructor()
        {
            Assert.AreEqual(3, _model.slideTable.nSizeNum);
            Assert.AreEqual(3, _model.slideTable.blackCounter);
            Assert.AreEqual(3, _model.slideTable.whiteCounter);
            Assert.AreEqual(5 * 3, _model.slideTable.circleNum);
            Assert.AreEqual(3, _model.slideTable.blackStones.Count);
            Assert.AreEqual(3, _model.slideTable.whiteStones.Count);
            Assert.AreEqual("black", _model.slideTable.currentPlayer);
        }
        [TestMethod]

        public void TestMatrixStonePlaces()
        {
            Assert.AreEqual(2, _model.slideTable.gameTable[0, 0]);
            Assert.AreEqual(1, _model.slideTable.gameTable[0, 1]);
            Assert.AreEqual(1, _model.slideTable.gameTable[0, 2]);
            Assert.AreEqual(1, _model.slideTable.gameTable[1, 0]);
            Assert.AreEqual(0, _model.slideTable.gameTable[1, 1]);
            Assert.AreEqual(0, _model.slideTable.gameTable[1, 2]);
            Assert.AreEqual(2, _model.slideTable.gameTable[2, 0]);
            Assert.AreEqual(0, _model.slideTable.gameTable[2, 1]);
            Assert.AreEqual(2, _model.slideTable.gameTable[2, 2]);
        }


        [TestMethod]

        public void TestSlide()
        {
            _model.SlideMove(Key.Left);
            Assert.AreEqual(1, _model.slideTable.gameTable[0, 0]);
            Assert.AreEqual(0, _model.slideTable.gameTable[0, 1]);
            Assert.AreEqual(1, _model.slideTable.gameTable[0, 2]);
            Assert.AreEqual("white", _model.slideTable.currentPlayer);
            _model.SlideMove(Key.Down);
            Assert.AreEqual(2, _model.slideTable.gameTable[2, 2]);
            Assert.AreEqual("white", _model.slideTable.currentPlayer);

        }

        [TestMethod]

        public void TestInvalidSteps()
        {
            Assert.AreEqual("black", _model.slideTable.currentPlayer);
            _model.SlideMove(Key.Up);
            _model.SlideMove(Key.Up);
            _model.SlideMove(Key.Up);
            Assert.AreEqual("black", _model.slideTable.currentPlayer);
            _model.SlideMove(Key.Down);
            Assert.AreEqual("white", _model.slideTable.currentPlayer);
            _model.SlideMove(Key.Left);
            _model.SlideMove(Key.Left);
            _model.SlideMove(Key.Left);
            Assert.AreEqual("white", _model.slideTable.currentPlayer);
            _model.SlideMove(Key.Right);
            Assert.AreEqual("black", _model.slideTable.currentPlayer);
        }

        [TestMethod]

        public void TestChangeSelectedStone()
        {
            // in second row the third black stone slides down the white stone under itself
            _model.changeSelectedStone();
            _model.changeSelectedStone();
            _model.SlideMove(Key.Down);
            Assert.AreEqual(1, _model.slideTable.gameTable[2, 0]);
            Assert.AreEqual(0, _model.slideTable.gameTable[1, 0]);

            // the second white stone (because the third one got eliminated) goes up one place
            _model.changeSelectedStone();
            _model.SlideMove(Key.Up);
            Assert.AreEqual(0, _model.slideTable.gameTable[2, 2]);
            Assert.AreEqual(2, _model.slideTable.gameTable[1, 2]);
        }

        [TestMethod]

        public void TestGameEndings()
        {
            _model.SlideMove(Key.Left);
            _model.changeSelectedStone();
            _model.SlideMove(Key.Up);
            _model.SlideMove(Key.Down);
            _model.SlideMove(Key.Down);
            _model.changeSelectedStone();
            _model.SlideMove(Key.Right);
            _model.SlideMove(Key.Left);
            _model.changeSelectedStone();
            Assert.AreEqual(false, _model.checkEndGame());
            _model.SlideMove(Key.Down);
            Assert.AreEqual(true, _model.checkEndGame());

        }


    }
}