using AutoMapper;
using projectCleanArch.Application.DTOs;
using projectCleanArch.Application.Interfaces;
using projectCleanArch.Domain.Entities;
using projectCleanArch.Domain.Interfaces;

namespace projectCleanArch.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this._categoryRepository = categoryRepository;
            this._mapper = mapper;
        }

        public async Task Add(CategoryDTO categoryDTO)
        {
            var CategoryEntity = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.CreateAsync(CategoryEntity);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categoriesEntity = await _categoryRepository.GetCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
        }

        public async Task<CategoryDTO> GetCategoryById(int id)
        {
            var categoryEntity = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryDTO>(categoryEntity);
        }

        public async Task Update(CategoryDTO categoryDTO)
        {
            var CategoryEntity = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.UpdateAsync(CategoryEntity);
        }
        public async Task Remove(int id)
        {
            var CategoryEntity = _categoryRepository.GetByIdAsync(id).Result;
            await _categoryRepository.RemoveAsync(CategoryEntity);
        }
    }
}
