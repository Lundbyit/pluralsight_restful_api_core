using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using pluralsight_restful_api_core.Entities;
using pluralsight_restful_api_core.Models;
using pluralsight_restful_api_core.Services;

namespace pluralsight_restful_api_core.Controllers
{
    [Route("api/authors/{authorId}/books")]
    public class BooksController : Controller
    {
        ILibraryRepository _libraryRepository { get; set; }
        public BooksController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }
        [HttpGet()]
        public IActionResult GetBooksForAuthor(Guid authorId)
        {
            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var booksFromRepo = _libraryRepository.GetBooksForAuthor(authorId);

            var books = Mapper.Map<IEnumerable<BookDto>>(booksFromRepo);
            return Ok(books);
        }

        [HttpGet("{bookId}", Name = "GetBookForAuthor")]
        public IActionResult GetBookForAuthor(Guid authorId, Guid bookId)
        {
            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var bookFromRepo = _libraryRepository.GetBookForAuthor(authorId, bookId);

            if (bookFromRepo == null)
            {
                return NotFound();
            }

            var bookResult = Mapper.Map<BookDto>(bookFromRepo);

            return Ok(bookResult);
        }

        [HttpPost()]
        public IActionResult CreateBookForAuthor(Guid authorId, [FromBody] BookForCreationDto newBook)
        {
            if (newBook == null)
            {
                return BadRequest();
            }

            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var newBookEntity = Mapper.Map<Book>(newBook);

            _libraryRepository.AddBookForAuthor(authorId, newBookEntity);

            if (!_libraryRepository.Save())
            {
                throw new Exception("Could not create new book for author");
            }

            var bookToReturn = Mapper.Map<BookDto>(newBookEntity);

            return CreatedAtRoute("GetBookForAuthor",
                new { authorId = bookToReturn.AuthorId, bookId = bookToReturn.Id },
                bookToReturn);
        }

        [HttpDelete("{bookId}")]
        public IActionResult DeleteBookForAuthor(Guid authorId, Guid bookId)
        {
            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var bookFromRepo = _libraryRepository.GetBookForAuthor(authorId, bookId);

            if (bookFromRepo == null)
            {
                return NotFound();
            }

            _libraryRepository.DeleteBook(bookFromRepo);
            if (!_libraryRepository.Save())
            {
                throw new Exception("Could not delete book");
            }

            return NoContent();
        }

        [HttpPut("{bookId}")]
        public IActionResult UpdateBookForAuthor(Guid authorId, Guid bookId,
            [FromBody] BookForUpdateDto book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var bookFromRepo = _libraryRepository.GetBookForAuthor(authorId, bookId);

            if (bookFromRepo == null)
            {
                var bookToAdd = Mapper.Map<Book>(book);
                bookToAdd.Id = bookId;

                _libraryRepository.AddBookForAuthor(authorId, bookToAdd);

                if (!_libraryRepository.Save())
                {
                    throw new Exception("Could not upsert book");
                }

                var bookToReturn = Mapper.Map<BookDto>(bookToAdd);

                return CreatedAtRoute("GetBookForAuthor",
                    new { authorId = bookToReturn.AuthorId, bookId = bookToReturn.Id },
                    bookToReturn);
            }

            Mapper.Map(book, bookFromRepo);
            _libraryRepository.UpdateBookForAuthor(bookFromRepo);

            if (!_libraryRepository.Save())
            {
                throw new Exception("Could not update book");
            }

            return Ok();
        }

        [HttpPatch("{bookId}")]
        public IActionResult PartiallyUpdateBookForAuthor(Guid authorId, Guid bookId,
            [FromBody] JsonPatchDocument<BookForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var bookFromRepo = _libraryRepository.GetBookForAuthor(authorId, bookId);

            if (bookFromRepo == null)
            {
                var bookDto = new BookForUpdateDto();

                patchDoc.ApplyTo(bookDto);
                var bookToAdd = Mapper.Map<Book>(bookDto);
                bookToAdd.Id = bookId;

                _libraryRepository.AddBookForAuthor(authorId, bookToAdd);

                if (!_libraryRepository.Save())
                {
                    throw new Exception("Could not upsert the book");
                }

                var bookToReturn = Mapper.Map<BookDto>(bookToAdd);

                return CreatedAtRoute("GetBookForAuthor",
                    new { authorId = bookToReturn.AuthorId, bookId = bookToReturn.Id },
                    bookToReturn);
            }

            var bookToPatch = Mapper.Map<BookForUpdateDto>(bookFromRepo);

            patchDoc.ApplyTo(bookToPatch);

            Mapper.Map(bookToPatch, bookFromRepo);

            _libraryRepository.UpdateBookForAuthor(bookFromRepo);

            if (!_libraryRepository.Save())
            {
                throw new Exception("failed to update book");
            }

            return NoContent();
        }
    }
}