﻿using pluralsight_restful_api_core.Entities;
using pluralsight_restful_api_core.Helpers;
using System;
using System.Collections.Generic;

namespace pluralsight_restful_api_core.Services
{
    public interface ILibraryRepository
    {
        PagedList<Author> GetAuthors(AuthorsResourceParameters authorResourceParameters);
        Author GetAuthor(Guid authorId);
        IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds);
        void AddAuthor(Author author);
        void DeleteAuthor(Author author);
        void UpdateAuthor(Author author);
        bool AuthorExists(Guid authorId);
        IEnumerable<Book> GetBooksForAuthor(Guid authorId);
        Book GetBookForAuthor(Guid authorId, Guid bookId);
        void AddBookForAuthor(Guid authorId, Book book);
        void UpdateBookForAuthor(Book book);
        void DeleteBook(Book book);
        bool Save();
    }
}
