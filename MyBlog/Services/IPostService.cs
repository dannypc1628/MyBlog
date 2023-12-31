﻿using MyBlog.Models;

namespace MyBlog.Services
{
    public interface IPostService
    {
        List<Post> GetAll(int page = 1, int pageSize = 10);

        Task<Post?> GetAsync(int id);

        Task<Post?> GetByPathAsync(string path);

        int Count();

        Task CreateAsync(Post post);

        Task UpdateAsync(Post post);

        void DeleteAsync(int id);
    }
}