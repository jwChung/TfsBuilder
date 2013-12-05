using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using Jwc.AutoFixture.Xunit;
using Jwc.TfsBuilder.WebApplication.Infrastructure;
using Jwc.TfsBuilder.WebApplication.Models;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.Framework.Client;
using Moq;
using Xunit;
using Xunit.Extensions;

namespace Jwc.TfsBuilder.WebApplication.Controllers
{
    public class TfsBuilderControllerTest
    {
        [Spec]
        public void SutIsController(
            [Build(BuildFlags.NoAutoProperties)] TfsBuilderController sut)
        {
            Assert.IsAssignableFrom<Controller>(sut);
        }

        [Spec]
        public void SutHasTfsBuilderActionTrackingAttribute()
        {
            var actual = typeof(TfsBuilderController).GetCustomAttribute<TfsBuilderActionTrackingAttribute>();

            Assert.NotNull(actual);
        }

        [Spec]
        [InlineData(null)]
        public void CtorWithNullBuildCommandThrows(
            [Inject] ICommand<BuildParameters> buildCommand,
            [Build] Lazy<TfsBuilderController> sut)
        {
            var e = Assert.Throws<TargetInvocationException>(() => sut.Value);
            var inner = Assert.IsType<ArgumentNullException>(e.InnerException);
            Assert.Equal("buildCommand", inner.ParamName);
        }

        [Spec]
        public void BuildCommandIsCorrect(
            [Build(BuildFlags.NoAutoProperties)] TfsBuilderController sut)
        {
            var actual = sut.BuildCommand;

            Assert.IsType<TfsBuildCommand>(actual);
        }

        [Spec]
        public void BuildCommandFromGreedyIsCorrect(
            [Inject] ICommand<BuildParameters> expected,
            [Build(BuildFlags.NoAutoProperties)] TfsBuilderController sut)
        {
            var actual = sut.BuildCommand;

            Assert.Equal(expected, actual);
        }

        [Spec]
        public void BuildHasCorrectHttpPostAttribute()
        {
            var method = typeof(TfsBuilderController).GetMethod("Build");
            Assert.NotNull(method);

            var actual = method.GetCustomAttribute<HttpPostAttribute>();

            Assert.NotNull(actual);
        }

        [Spec]
        public void BuildHasCorrectValidateInputAttribute()
        {
            var method = typeof(TfsBuilderController).GetMethod("Build");
            Assert.NotNull(method);

            var actual = method.GetCustomAttribute<ValidateInputAttribute>();

            Assert.NotNull(actual);
            Assert.False(actual.EnableValidation, "EnableValidation");
        }

        [Spec]
        public void BuildHasCorrectRequireHttpsAttribute()
        {
            var method = typeof(TfsBuilderController).GetMethod("Build");
            Assert.NotNull(method);

            var actual = method.GetCustomAttribute<RequireAppHarborHttpsAttribute>();

            Assert.NotNull(actual);
        }

        [Spec]
        public void BuildWithNullBuildParametersThrows(
            [Build(BuildFlags.NoAutoProperties)] TfsBuilderController sut,
            string payload)
        {
            var e = Assert.Throws<ArgumentNullException>(() => sut.Build(null));
            Assert.Equal("parameters", e.ParamName);
        }

        [Spec]
        public void BuildWithInvalidBuildParametersShowErroMessagesOnWebPage(
            [Build(BuildFlags.NoAutoProperties)] TfsBuilderController sut,
            BuildParameters parameters,
            string expected1,
            string expected2)
        {
            // Arrange
            sut.ModelState.AddModelError("dummy1", expected1);
            sut.ModelState.AddModelError("dummy2", expected2);

            // Act
            var actual = sut.Build(parameters);

            // Assert
            var contentResult = Assert.IsType<ContentResult>(actual);
            Assert.Contains(expected1, contentResult.Content);
            Assert.Contains(expected2, contentResult.Content);
        }

        private class CommitDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new object[] { @"{""ref"":""refs/heads/master"",""after"":""cde762d72cf8658b0a8ba4b659a025d48f181bb6"",""before"":""5818e1a160545af321abf2f1c8410e18e3a1c007"",""created"":false,""deleted"":false,""forced"":false,""compare"":""https://github.com/jwChung/AutoFixture.Contrib/compare/5818e1a16054...cde762d72cf8"",""commits"":[{""id"":""0161c1b8bf033b459a43d5213ee9803ebd027f54"",""distinct"":true,""message"":""Addressed the code analysis errors."",""timestamp"":""2013-11-05T05:43:34-08:00"",""url"":""https://github.com/jwChung/AutoFixture.Contrib/commit/0161c1b8bf033b459a43d5213ee9803ebd027f54"",""author"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""committer"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""added"":[],""removed"":[],""modified"":[""src/AutoFixture.Contrib/Xunit/NamedFrozenAttribute.cs""]},{""id"":""cde762d72cf8658b0a8ba4b659a025d48f181bb6"",""distinct"":true,""message"":""Incremented pre-release version number\n\nto indicate that the NamedFrozenAttribute is new feature."",""timestamp"":""2013-11-05T05:44:14-08:00"",""url"":""https://github.com/jwChung/AutoFixture.Contrib/commit/cde762d72cf8658b0a8ba4b659a025d48f181bb6"",""author"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""committer"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""added"":[],""removed"":[],""modified"":[""build/CommonAssemblyInfo.cs""]}],""head_commit"":{""id"":""cde762d72cf8658b0a8ba4b659a025d48f181bb6"",""distinct"":true,""message"":""Incremented pre-release version number\n\nto indicate that the NamedFrozenAttribute is new feature."",""timestamp"":""2013-11-05T05:44:14-08:00"",""url"":""https://github.com/jwChung/AutoFixture.Contrib/commit/cde762d72cf8658b0a8ba4b659a025d48f181bb6"",""author"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""committer"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""added"":[],""removed"":[],""modified"":[""build/CommonAssemblyInfo.cs""]},""repository"":{""id"":10662835,""name"":""AutoFixture.Contrib"",""url"":""https://github.com/jwChung/AutoFixture.Contrib"",""description"":""AutoFixture.Contrib"",""watchers"":2,""stargazers"":2,""forks"":0,""fork"":false,""size"":11480,""owner"":{""name"":""jwChung"",""email"":""mail@mail.com""},""private"":false,""open_issues"":0,""has_issues"":true,""has_downloads"":true,""has_wiki"":true,""language"":""C#"",""created_at"":1371115276,""pushed_at"":1383662039,""master_branch"":""master""},""pusher"":{""name"":""jwChung"",""email"":""mail@mail.com""}}" };
                yield return new object[] { @"{""ref"":""refs/heads/master"",""after"":""ef6d40f2d60e82af047b45d9a2df166f520dcf9e"",""before"":""ecd39a427ccce2dfa443b8679e407f434a45dbc1"",""created"":false,""deleted"":false,""forced"":false,""compare"":""https://github.com/jwChung/AutoFixture.Contrib/compare/ecd39a427ccc...ef6d40f2d60e"",""commits"":[{""id"":""1f6f24dce7414ddad9631f64c2d5cc3be7190033"",""distinct"":true,""message"":""Changed the assembly names."",""timestamp"":""2013-11-05T20:34:16-08:00"",""url"":""https://github.com/jwChung/AutoFixture.Contrib/commit/1f6f24dce7414ddad9631f64c2d5cc3be7190033"",""author"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""committer"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""added"":[],""removed"":[],""modified"":[""src/AutoFixture.Contrib/AutoFixture.Contrib.csproj"",""test/AutoFixture.Contrib.UnitTest/AutoFixture.Contrib.UnitTest.csproj""]},{""id"":""c51c2424a4e28496d1e6c9a8ec1314ce724f10bd"",""distinct"":true,""message"":""Imiplmented reflection."",""timestamp"":""2013-11-05T20:34:16-08:00"",""url"":""https://github.com/jwChung/AutoFixture.Contrib/commit/c51c2424a4e28496d1e6c9a8ec1314ce724f10bd"",""author"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""committer"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""added"":[""src/AutoFixture.Contrib/MemberCollection.cs"",""src/AutoFixture.Contrib/MemberCollectionExtensions.cs"",""src/AutoFixture.Contrib/Reflector.cs"",""src/AutoFixture.Contrib/ReflectorException.cs"",""test/AutoFixture.Contrib.UnitTest/AutoMoqDataAttribute.cs"",""test/AutoFixture.Contrib.UnitTest/MemberCollectionExtensionsTest.cs"",""test/AutoFixture.Contrib.UnitTest/MemberCollectionTest.cs"",""test/AutoFixture.Contrib.UnitTest/MembersTestType.cs"",""test/AutoFixture.Contrib.UnitTest/MembersTestTypeBase.cs"",""test/AutoFixture.Contrib.UnitTest/ReflectorTest.cs""],""removed"":[],""modified"":[""src/AutoFixture.Contrib/AutoFixture.Contrib.csproj"",""src/AutoFixture.Contrib/Xunit/AutoDataTheoryCommand.cs"",""test/AutoFixture.Contrib.UnitTest/AutoFixture.Contrib.UnitTest.csproj"",""test/AutoFixture.Contrib.UnitTest/Xunit/AutoDataTheoryCommandTest.cs""]},{""id"":""30fa8b6fb3a7424a64f2f5f823e3cb728c3f44a6"",""distinct"":true,""message"":""Renamed MembersTestType\n\nto MemberCollectionTestType."",""timestamp"":""2013-11-05T20:34:17-08:00"",""url"":""https://github.com/jwChung/AutoFixture.Contrib/commit/30fa8b6fb3a7424a64f2f5f823e3cb728c3f44a6"",""author"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""committer"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""added"":[""test/AutoFixture.Contrib.UnitTest/MemberCollectionTestType.cs"",""test/AutoFixture.Contrib.UnitTest/MemberCollectionTestTypeBase.cs""],""removed"":[""test/AutoFixture.Contrib.UnitTest/MembersTestType.cs"",""test/AutoFixture.Contrib.UnitTest/MembersTestTypeBase.cs""],""modified"":[""test/AutoFixture.Contrib.UnitTest/AutoFixture.Contrib.UnitTest.csproj"",""test/AutoFixture.Contrib.UnitTest/MemberCollectionExtensionsTest.cs"",""test/AutoFixture.Contrib.UnitTest/MemberCollectionTest.cs"",""test/AutoFixture.Contrib.UnitTest/ReflectorTest.cs""]},{""id"":""0b55df3d1c4bfd49016482bc47ca650c4b62d278"",""distinct"":true,""message"":""Deleted the Of method\n\nand the public contuctor will be used instead."",""timestamp"":""2013-11-05T20:34:17-08:00"",""url"":""https://github.com/jwChung/AutoFixture.Contrib/commit/0b55df3d1c4bfd49016482bc47ca650c4b62d278"",""author"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""committer"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""added"":[],""removed"":[],""modified"":[""src/AutoFixture.Contrib/MemberCollection.cs"",""src/AutoFixture.Contrib/MemberCollectionExtensions.cs"",""test/AutoFixture.Contrib.UnitTest/MemberCollectionExtensionsTest.cs"",""test/AutoFixture.Contrib.UnitTest/MemberCollectionTest.cs""]},{""id"":""8b6c0627903aa16ca8c6346f23160b83ca040285"",""distinct"":true,""message"":""Deleted the Of(BindingFlags) method\n\nand the public contuctor will be used instead."",""timestamp"":""2013-11-05T20:34:18-08:00"",""url"":""https://github.com/jwChung/AutoFixture.Contrib/commit/8b6c0627903aa16ca8c6346f23160b83ca040285"",""author"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""committer"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""added"":[],""removed"":[],""modified"":[""src/AutoFixture.Contrib/MemberCollection.cs"",""test/AutoFixture.Contrib.UnitTest/MemberCollectionTest.cs""]},{""id"":""6606d7f1290198c5a164e3ec38f2aa89ae538ffb"",""distinct"":true,""message"":""Reformatted XML documents."",""timestamp"":""2013-11-05T20:34:18-08:00"",""url"":""https://github.com/jwChung/AutoFixture.Contrib/commit/6606d7f1290198c5a164e3ec38f2aa89ae538ffb"",""author"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""committer"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""added"":[],""removed"":[],""modified"":[""src/AutoFixture.Contrib/MemberCollection.cs"",""src/AutoFixture.Contrib/MemberCollectionExtensions.cs"",""src/AutoFixture.Contrib/Reflector.cs"",""src/AutoFixture.Contrib/ReflectorException.cs"",""src/AutoFixture.Contrib/Xunit/AutoDataTheoryAttribute.cs"",""src/AutoFixture.Contrib/Xunit/AutoDataTheoryCommand.cs"",""src/AutoFixture.Contrib/Xunit/CustomizationFixtureAttribute.cs"",""src/AutoFixture.Contrib/Xunit/FixtureAttribute.cs"",""test/AutoFixture.Contrib.UnitTest/Xunit/AutoDataTheoryCommandTest.cs""]},{""id"":""09d7eb575b42113490d984b19132e717b863345d"",""distinct"":true,""message"":""Renamed AutoMoqDataAttribute\n\nto AutoFixtureDataAttribute."",""timestamp"":""2013-11-05T20:34:19-08:00"",""url"":""https://github.com/jwChung/AutoFixture.Contrib/commit/09d7eb575b42113490d984b19132e717b863345d"",""author"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""committer"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""added"":[""test/AutoFixture.Contrib.UnitTest/AutoFixtureDataAttribute.cs""],""removed"":[""test/AutoFixture.Contrib.UnitTest/AutoMoqDataAttribute.cs""],""modified"":[""test/AutoFixture.Contrib.UnitTest/AutoFixture.Contrib.UnitTest.csproj"",""test/AutoFixture.Contrib.UnitTest/MemberCollectionTest.cs""]},{""id"":""fd9f3b0b40056595752a9bdcfc589a7e844e64eb"",""distinct"":true,""message"":""Made the Reflection namespace."",""timestamp"":""2013-11-05T20:34:19-08:00"",""url"":""https://github.com/jwChung/AutoFixture.Contrib/commit/fd9f3b0b40056595752a9bdcfc589a7e844e64eb"",""author"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""committer"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""added"":[""src/AutoFixture.Contrib/Reflection/MemberCollection.cs"",""src/AutoFixture.Contrib/Reflection/MemberCollectionExtensions.cs"",""src/AutoFixture.Contrib/Reflection/Reflector.cs"",""src/AutoFixture.Contrib/Reflection/ReflectorException.cs"",""test/AutoFixture.Contrib.UnitTest/Reflection/MemberCollectionExtensionsTest.cs"",""test/AutoFixture.Contrib.UnitTest/Reflection/MemberCollectionTest.cs"",""test/AutoFixture.Contrib.UnitTest/Reflection/MemberCollectionTestType.cs"",""test/AutoFixture.Contrib.UnitTest/Reflection/MemberCollectionTestTypeBase.cs"",""test/AutoFixture.Contrib.UnitTest/Reflection/ReflectorTest.cs""],""removed"":[""src/AutoFixture.Contrib/MemberCollection.cs"",""src/AutoFixture.Contrib/MemberCollectionExtensions.cs"",""src/AutoFixture.Contrib/Reflector.cs"",""src/AutoFixture.Contrib/ReflectorException.cs"",""test/AutoFixture.Contrib.UnitTest/MemberCollectionExtensionsTest.cs"",""test/AutoFixture.Contrib.UnitTest/MemberCollectionTest.cs"",""test/AutoFixture.Contrib.UnitTest/MemberCollectionTestType.cs"",""test/AutoFixture.Contrib.UnitTest/MemberCollectionTestTypeBase.cs"",""test/AutoFixture.Contrib.UnitTest/ReflectorTest.cs""],""modified"":[""src/AutoFixture.Contrib/AutoFixture.Contrib.csproj"",""test/AutoFixture.Contrib.UnitTest/AutoFixture.Contrib.UnitTest.csproj""]},{""id"":""28a931938399ceadf606748280da6892532052dd"",""distinct"":true,""message"":""Removed all the suppressions of StyleCop."",""timestamp"":""2013-11-05T20:34:20-08:00"",""url"":""https://github.com/jwChung/AutoFixture.Contrib/commit/28a931938399ceadf606748280da6892532052dd"",""author"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""committer"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""added"":[],""removed"":[],""modified"":[""src/AutoFixture.Contrib/Reflection/MemberCollection.cs"",""test/AutoFixture.Contrib.UnitTest/Reflection/MemberCollectionTest.cs"",""test/AutoFixture.Contrib.UnitTest/Reflection/MemberCollectionTestType.cs"",""test/AutoFixture.Contrib.UnitTest/Reflection/MemberCollectionTestTypeBase.cs"",""test/AutoFixture.Contrib.UnitTest/Xunit/AutoDataTheoryCommandExecuteTest.cs""]},{""id"":""a53d37f485759e1fff8f7b98f39a7a601985dbdc"",""distinct"":true,""message"":""Removed unused using directives."",""timestamp"":""2013-11-05T20:43:20-08:00"",""url"":""https://github.com/jwChung/AutoFixture.Contrib/commit/a53d37f485759e1fff8f7b98f39a7a601985dbdc"",""author"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""committer"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""added"":[],""removed"":[],""modified"":[""src/AutoFixture.Contrib/Reflection/MemberCollection.cs"",""test/AutoFixture.Contrib.UnitTest/Reflection/MemberCollectionTest.cs"",""test/AutoFixture.Contrib.UnitTest/Reflection/MemberCollectionTestType.cs"",""test/AutoFixture.Contrib.UnitTest/Reflection/MemberCollectionTestTypeBase.cs"",""test/AutoFixture.Contrib.UnitTest/Xunit/AutoDataTheoryCommandExecuteTest.cs""]},{""id"":""ef6d40f2d60e82af047b45d9a2df166f520dcf9e"",""distinct"":true,""message"":""Incremented pre-release version,\n\nto indicate that MemberCollecion<T> is new feature."",""timestamp"":""2013-11-05T20:43:34-08:00"",""url"":""https://github.com/jwChung/AutoFixture.Contrib/commit/ef6d40f2d60e82af047b45d9a2df166f520dcf9e"",""author"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""committer"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""added"":[],""removed"":[],""modified"":[""build/CommonAssemblyInfo.cs""]}],""head_commit"":{""id"":""ef6d40f2d60e82af047b45d9a2df166f520dcf9e"",""distinct"":true,""message"":""Incremented pre-release version,\n\nto indicate that MemberCollecion<T> is new feature."",""timestamp"":""2013-11-05T20:43:34-08:00"",""url"":""https://github.com/jwChung/AutoFixture.Contrib/commit/ef6d40f2d60e82af047b45d9a2df166f520dcf9e"",""author"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""committer"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""added"":[],""removed"":[],""modified"":[""build/CommonAssemblyInfo.cs""]},""repository"":{""id"":10662835,""name"":""AutoFixture.Contrib"",""url"":""https://github.com/jwChung/AutoFixture.Contrib"",""description"":""AutoFixture.Contrib"",""watchers"":2,""stargazers"":2,""forks"":0,""fork"":false,""size"":1248,""owner"":{""name"":""jwChung"",""email"":""mail@mail.com""},""private"":false,""open_issues"":0,""has_issues"":true,""has_downloads"":true,""has_wiki"":true,""language"":""C#"",""created_at"":1371115276,""pushed_at"":1383714003,""master_branch"":""master""},""pusher"":{""name"":""jwChung"",""email"":""mail@mail.com""}}" };
            }
        }

        [Spec]
        [CommitData]
        [InlineData]
        public void BuildWithCommitsExecutesBuildCommandAndReturns404Error(
            [Inject(Matches.SameName)] string payload,  // to parameters
            [Inject] ICommand<BuildParameters> buildCommand, // to sut
            [Build(BuildFlags.NoAutoProperties)] TfsBuilderController sut,
            [Build] BuildParameters parameters)
        {
            var actual = sut.Build(parameters);

            Mock.Get(buildCommand).Verify(x => x.Execute(parameters));
            var contentResult = Assert.IsType<ContentResult>(actual);
            Assert.Equal("Just have queued a new build process.", contentResult.Content);
        }

        private class NonCommitDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new object[] { @"{""ref"":""refs/tags/2.0.0-pre05"",""after"":""cde762d72cf8658b0a8ba4b659a025d48f181bb6"",""before"":""0000000000000000000000000000000000000000"",""created"":true,""deleted"":false,""forced"":true,""base_ref"":""refs/heads/master"",""compare"":""https://github.com/jwChung/AutoFixture.Contrib/compare/2.0.0-pre05"",""commits"":[],""head_commit"":{""id"":""cde762d72cf8658b0a8ba4b659a025d48f181bb6"",""distinct"":true,""message"":""Incremented pre-release version number\n\nto indicate that the NamedFrozenAttribute is new feature."",""timestamp"":""2013-11-05T05:44:14-08:00"",""url"":""https://github.com/jwChung/AutoFixture.Contrib/commit/cde762d72cf8658b0a8ba4b659a025d48f181bb6"",""author"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""committer"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""added"":[],""removed"":[],""modified"":[""build/CommonAssemblyInfo.cs""]},""repository"":{""id"":10662835,""name"":""AutoFixture.Contrib"",""url"":""https://github.com/jwChung/AutoFixture.Contrib"",""description"":""AutoFixture.Contrib"",""watchers"":2,""stargazers"":2,""forks"":0,""fork"":false,""size"":11480,""owner"":{""name"":""jwChung"",""email"":""mail@mail.com""},""private"":false,""open_issues"":0,""has_issues"":true,""has_downloads"":true,""has_wiki"":true,""language"":""C#"",""created_at"":1371115276,""pushed_at"":1383661487,""master_branch"":""master""},""pusher"":{""name"":""jwChung"",""email"":""mail@mail.com""}}" };
                yield return new object[] { @"{""ref"":""refs/tags/2.0.0-pre05"",""after"":""0000000000000000000000000000000000000000"",""before"":""cde762d72cf8658b0a8ba4b659a025d48f181bb6"",""created"":false,""deleted"":true,""forced"":true,""compare"":""https://github.com/jwChung/AutoFixture.Contrib/compare/cde762d72cf8...000000000000"",""commits"":[],""head_commit"":null,""repository"":{""id"":10662835,""name"":""AutoFixture.Contrib"",""url"":""https://github.com/jwChung/AutoFixture.Contrib"",""description"":""AutoFixture.Contrib"",""watchers"":2,""stargazers"":2,""forks"":0,""fork"":false,""size"":11480,""owner"":{""name"":""jwChung"",""email"":""mail@mail.com""},""private"":false,""open_issues"":0,""has_issues"":true,""has_downloads"":true,""has_wiki"":true,""language"":""C#"",""created_at"":1371115276,""pushed_at"":1383661684,""master_branch"":""master""},""pusher"":{""name"":""jwChung"",""email"":""mail@mail.com""}}" };
                yield return new object[] { @"{""ref"":""refs/heads/master"",""after"":""5818e1a160545af321abf2f1c8410e18e3a1c007"",""before"":""cde762d72cf8658b0a8ba4b659a025d48f181bb6"",""created"":false,""deleted"":false,""forced"":true,""compare"":""https://github.com/jwChung/AutoFixture.Contrib/compare/cde762d72cf8...5818e1a16054"",""commits"":[],""head_commit"":{""id"":""5818e1a160545af321abf2f1c8410e18e3a1c007"",""distinct"":true,""message"":""Changed code analysis rule for the test project\n\nto the mininum."",""timestamp"":""2013-11-05T05:41:20-08:00"",""url"":""https://github.com/jwChung/AutoFixture.Contrib/commit/5818e1a160545af321abf2f1c8410e18e3a1c007"",""author"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""committer"":{""name"":""Jin-Wook Chung"",""email"":""mail@mail.com"",""username"":""jwChung""},""added"":[],""removed"":[],""modified"":[""test/AutoFixture.Contrib.UnitTest/AutoFixture.Contrib.UnitTest.csproj""]},""repository"":{""id"":10662835,""name"":""AutoFixture.Contrib"",""url"":""https://github.com/jwChung/AutoFixture.Contrib"",""description"":""AutoFixture.Contrib"",""watchers"":2,""stargazers"":2,""forks"":0,""fork"":false,""size"":11480,""owner"":{""name"":""jwChung"",""email"":""mail@mail.com""},""private"":false,""open_issues"":0,""has_issues"":true,""has_downloads"":true,""has_wiki"":true,""language"":""C#"",""created_at"":1371115276,""pushed_at"":1383661978,""master_branch"":""master""},""pusher"":{""name"":""jwChung"",""email"":""mail@mail.com""}}" };
            }
        }

        [Spec]
        [NonCommitData]
        public void BuildWithNoCommitsDoesNotExecuteBulidCommandAndReturns404Error(
            [Inject(Matches.SameName)] string payload,  // to parameters
            [Inject] ICommand<BuildParameters> buildCommand, // to sut
            [Build(BuildFlags.NoAutoProperties)] TfsBuilderController sut,
            [Build] BuildParameters parameters)
        {
            var actual = sut.Build(parameters);

            Mock.Get(buildCommand).Verify(x => x.Execute(parameters), Times.Never());
            var contentResult = Assert.IsType<ContentResult>(actual);
            Assert.Equal("There are no commits to queue a build process.", contentResult.Content);
        }

        private class BuildExceptionDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new object[] { new TeamFoundationServiceUnavailableException("reason") };
                yield return new object[] { new ProjectDoesNotExistWithNameException("message") };
                yield return new object[] { new TfsBuildException() };
                yield return new object[] { new TeamFoundationServerUnauthorizedException() };
                yield return new object[] { new UriFormatException() };
            }
        }

        [Spec]
        [BuildExceptionData]
        public void BuildShowsMessageOfExceptionThrownFromBuildCommand(
            Exception exception,
            [Inject(Matches.SameName)] string payload, // to parameters
            [Inject] ICommand<BuildParameters> buildCommand, // to sut
            [Build(BuildFlags.NoAutoProperties)] TfsBuilderController sut,
            [Build] BuildParameters parameters)
        {
            Mock.Get(buildCommand).Setup(x => x.Execute(parameters)).Throws(exception);

            var actual = sut.Build(parameters);

            var contentResult = Assert.IsType<ContentResult>(actual);
            Assert.Equal(exception.Message, contentResult.Content);
        }
    }
}