using FluentAssertions;
using projectCleanArch.Domain.Entities;

namespace projectCleanArch.Domain.Tests
{
    public class CategoryUnitTest1
    {
        [Fact(DisplayName ="Create Category With Valid State")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action= () => new Category(1,"Category Name");
            action.Should()
                .NotThrow<projectCleanArch.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Category With Invalid Id")]
        public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Category(-1, "Category Name");
            action.Should()
                .Throw<projectCleanArch.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid id value");
        }

        [Fact(DisplayName = "Create Category With Invalid Name")]
        public void CreateCategory_ShortNameValue_DomainExceptionInvalidId()
        {
            Action action = () => new Category(1, "Ca");
            action.Should()
                .Throw<projectCleanArch.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Name, too short, minimum 3 charecters");
        }

        [Fact(DisplayName = "Create Category Without Name")]
        public void CreateCategory_WithoutNameValue_DomainExceptionInvalidId()
        {
            Action action = () => new Category(1, "");
            action.Should()
                .Throw<projectCleanArch.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid, Name is required");
        }

        [Fact(DisplayName = "Create Category With null Name")]
        public void CreateCategory_WithNullNameValue_DomainExceptionInvalidId()
        {
            Action action = () => new Category(1, null);
            action.Should()
                .Throw<projectCleanArch.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid, Name is required");
        }
    }
}