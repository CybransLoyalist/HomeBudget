using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Mvc;
using HomeBudget.Controllers.YearSheets;
using HomeBudget.Models;
using HomeBudget.Models.Repositories;
using Moq;
using NUnit.Framework;

namespace HomeBudgetTests.Controllers.YearSheets
{
    [TestFixture]
    class YearSheetRelatedDataRemoverTests
    {
        private YearSheetRelatedDataRemover _sut;
        private Mock<YearSheetsRepository> _yearSheetsRepositoryMock;
        private Mock<SheetsRepository> _sheetsRepositoryMock;
        private const int YearSheetId = 6;

        [SetUp]
        public void SetUp()
        {
            _yearSheetsRepositoryMock = new Mock<YearSheetsRepository>(null);
            _sheetsRepositoryMock = new Mock<SheetsRepository>(null);
            _sut = new YearSheetRelatedDataRemover(_yearSheetsRepositoryMock.Object, _sheetsRepositoryMock.Object);
        }

        [Test]
        public void ShallRemoveAllSheetsAndSaveChanges()
        {
            var relatedSheets = new List<Sheet>
            {
                new Sheet(),
                new Sheet()
            };
            _sheetsRepositoryMock.Setup(a => a.GetAllForYearSheet(YearSheetId)).Returns(relatedSheets);

            _sut.RemoveFor(YearSheetId);

            _sheetsRepositoryMock.Verify(a => a.Remove(It.IsAny<Sheet>()), Times.Exactly(relatedSheets.Count));
            _sheetsRepositoryMock.Verify(a => a.SaveChanges());
        }

        [Test]
        public void ShallDeleteGivenYearSheet_andSaveChanges()
        {
            var yearSheet = new YearSheet();
            _yearSheetsRepositoryMock.Setup(a => a.GetById(YearSheetId)).Returns(yearSheet);

            _sut.RemoveFor(YearSheetId);

            _yearSheetsRepositoryMock.Verify(a => a.GetById(YearSheetId));
            _yearSheetsRepositoryMock.Verify(a => a.Remove(yearSheet));
            _yearSheetsRepositoryMock.Verify(a => a.SaveChanges());
        }
    }

    [TestFixture]
    class YearSheetControllerTests : ControlletTests
    {
        private YearSheetController _sut;
        private Mock<YearSheetsRepository> _yearSheetsRepositoryMock;
        private Mock<YearSheetRelatedDataRemover> _yearSheetRelatedDataRemoverMock;
        private Mock<YearSheetCreator> _yearSheetCreatorMock;
        private Mock<ControllerContext> _controllerContextMock;

        [SetUp]
        public void SetUp()
        {
            _yearSheetsRepositoryMock = new Mock<YearSheetsRepository>(null);
            _yearSheetCreatorMock = new Mock<YearSheetCreator>(null);
            _yearSheetRelatedDataRemoverMock = new Mock<YearSheetRelatedDataRemover>(null, null);

            _sut = new YearSheetController(
                _yearSheetsRepositoryMock.Object,
                _yearSheetCreatorMock.Object,
                _yearSheetRelatedDataRemoverMock.Object);

            _controllerContextMock = new Mock<ControllerContext>();
            _controllerContextMock.SetupGet(p => p.HttpContext.User.Identity).Returns(new Mock<IIdentity>().Object);
            _sut.ControllerContext = _controllerContextMock.Object;
        }

        [Test]
        public void NoYearSheetPresent_ShallCreateView()
        {
            var result = _sut.NoYearSheetPresent();
            Assert.NotNull(result);
        }

        [Test]
        public void Current_ShallGetCurrentYearSheetForUser_AndGetViewWithThisSheetAsModel()
        {
            var yearSheet = new YearSheet();
            _yearSheetsRepositoryMock.Setup(a => a.GetForUser(It.IsAny<string>())).Returns(yearSheet);

            var result = _sut.Current();

            _yearSheetsRepositoryMock.Verify(a => a.GetForUser(It.IsAny<string>()));
            Assert.AreEqual(yearSheet, (result as ViewResult).Model);
        }

        [Test]
        public void GetNewYearSheetCreatingPage_ShallCreateBasicYearSheet_AndGetEditViewWithThisSheetAsModel()
        {
            var yearSheet = new YearSheet();
            _yearSheetCreatorMock.Setup(a => a.CreateBasic(It.IsAny<IPrincipal>())).Returns(yearSheet);

            var result = _sut.GetNewYearSheetCreatingPage();

            _yearSheetCreatorMock.Verify(a => a.CreateBasic(It.IsAny<IPrincipal>()));
            Assert.AreEqual(yearSheet, (result as ViewResult).Model);
            Assert.AreEqual("Edit", (result as ViewResult).ViewName);
        }

        [Test]
        public void ViewYearSheet_ShallGetYearSheetFromRepository_AndPassItAsModelTo_YearSheetView()
        {
            var yearSheet = new YearSheet();
            _yearSheetsRepositoryMock.Setup(a => a.GetById(3)).Returns(yearSheet);

            var result = _sut.ViewYearSheet(3);

            _yearSheetsRepositoryMock.Verify(a => a.GetById(3));
            Assert.AreEqual(yearSheet, (result as ViewResult).Model);
            Assert.AreEqual("YearSheetView", (result as ViewResult).ViewName);
        }

        [Test]
        public void Save_ShallGetEditViewBack_IfModelStateNotValid()
        {
            MakeModelStateInvalid(_sut);

            var yearSheet = new YearSheet();
            var result = _sut.Save(yearSheet);

            Assert.AreEqual(yearSheet, (result as ViewResult).Model);
            Assert.AreEqual("Edit", (result as ViewResult).ViewName);
        }

        [Test]
        public void Save_ShallSetModelAsModified_IfModelStateValid()
        {
            var yearSheet = new YearSheet();
            var result = _sut.Save(yearSheet);

            _yearSheetsRepositoryMock.Verify(a => a.SetModified(yearSheet));
        }

        [Test]
        public void Save_ShallSaveChanges_IfModelStateValid()
        {
            var yearSheet = new YearSheet();
            var result = _sut.Save(yearSheet);

            _yearSheetsRepositoryMock.Verify(a => a.SaveChanges());
        }

        [Test]
        public void Save_ShallRedirectTo_Index_IfModelStateValid()
        {
            var yearSheet = new YearSheet();
            var result = _sut.Save(yearSheet);
            
            Assert.AreEqual("Index", (result as ViewResult).ViewName);
        }

        [Test]
        public void Edit_ShallGetYearSheetFromRepository_AndPassItAsModelTo_EditView()
        {
            var yearSheet = new YearSheet();
            _yearSheetsRepositoryMock.Setup(a => a.GetById(3)).Returns(yearSheet);

            var result = _sut.Edit(3);

            _yearSheetsRepositoryMock.Verify(a => a.GetById(3));
            Assert.AreEqual(yearSheet, (result as ViewResult).Model);
            Assert.AreEqual("Edit", (result as ViewResult).ViewName);
        }

        [Test]
        public void Delete_ShallRemoveRelatedData()
        {
            _sut.Delete(2);
            _yearSheetRelatedDataRemoverMock.Verify(a => a.RemoveFor(2));
        }

        [Test]
        public void Delete_ShallReloadYearSheetList_AndShowIt()
        {
            var yearSheets = new List<YearSheet>();
            _yearSheetsRepositoryMock.Setup(a => a.GetAllForUser(It.IsAny<string>())).Returns(yearSheets);

            var result = _sut.Delete(2);

            Assert.AreEqual(yearSheets, (result as ViewResult).Model);
            Assert.AreEqual("Index", (result as ViewResult).ViewName);
        }
    }
}