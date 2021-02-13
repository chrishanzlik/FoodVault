using FoodVault.Framework.Application.FileUploads;
using System;
using Xunit;

namespace FoodVault.Framework.UnitTests.FileUploads
{
    public class DashFileNameSanitizerTests
    {
        [Theory]
        [InlineData("FooBar123",                    "FooBar123")]
        [InlineData("test!",                        "test")]
        [InlineData("$some-special/string.net",     "some-special-string-net")]
        [InlineData("duplicated!!$invalid-chars",   "duplicated-invalid-chars")]
        [InlineData("file.",                        "file")]
        [InlineData("file...",                      "file")]
        public void Sanitize_TransformsTheFileNameToExpectedResult_WithoutExtension(string fileName, string expected)
        {
            var sut = new DashFileNameSanitizer();
            var sanitized = sut.Sanitize(fileName, string.Empty);

            Assert.Equal(expected, sanitized);
        }

        [Theory]
        [InlineData("FooBar123",    "JPG",      "FooBar123.JPG")]
        [InlineData("test!",        "pdf",      "test.pdf")]
        [InlineData("$test!123",    ".Png",     "test-123.Png")]
        public void Sanitize_AppendsFileExtension_WhenExtensionIsGiven(string fileName, string extension, string expected)
        {
            var sut = new DashFileNameSanitizer();
            var sanitized = sut.Sanitize(fileName, extension);

            Assert.Equal(expected, sanitized);
        }

        [Theory]
        [InlineData((string)null)]
        [InlineData(" ")]
        public void Sanitize_ThrowsArgumentException_WhenFileNameIsNullOrWhitespace(string fileName)
        {
            var sut = new DashFileNameSanitizer();

            Assert.Throws<ArgumentException>(() => sut.Sanitize(fileName, null));
        }
    }
}
