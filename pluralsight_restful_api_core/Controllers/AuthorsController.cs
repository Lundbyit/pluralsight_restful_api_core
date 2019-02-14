using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pluralsight_restful_api_core.Entities;
using pluralsight_restful_api_core.Models;
using pluralsight_restful_api_core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace pluralsight_restful_api_core.Controllers
{
    [Route("api/authors")]
    public class AuthorsController : Controller
    {
        private ILibraryRepository _libraryRepository;
        public AuthorsController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpGet()]
        public IActionResult GetAuthors()
        {
                var authorsFromRepo = _libraryRepository.GetAuthors().ToList();
                var authors = Mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo);
                return Ok(authors);
        }

        [HttpGet("{authorId}", Name = "GetAuthor")]
        public IActionResult GetAuthor(Guid authorId)
        {
            var authorFromRepo = _libraryRepository.GetAuthor(authorId);

            if (authorFromRepo == null)
            {
                return NotFound();
            }

            var author = Mapper.Map<AuthorDto>(authorFromRepo);

            return Ok(author);
        }

        [HttpPost()]
        public IActionResult CreateAuthor([FromBody] AuthorForCreationDto author)
        {
            if (author == null)
            {
                return BadRequest();
            }

            var newAuthorEntity = Mapper.Map<Author>(author);

            _libraryRepository.AddAuthor(newAuthorEntity);

            if (!_libraryRepository.Save())
            {
                throw new Exception("Failed to create author");
            }

            var authorToReturn = Mapper.Map<AuthorDto>(newAuthorEntity);
            
            return CreatedAtRoute("GetAuthor", new { authorId = authorToReturn.Id }, authorToReturn);
        }

        [HttpPost("{authorId}")]
        public IActionResult BlockAuthorCreation(Guid authorId)
        {
            if (_libraryRepository.AuthorExists(authorId))
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            return NotFound();
        }

        [HttpDelete("{authorId}")]
        public IActionResult DeleteAuthor(Guid authorId)
        {
            var authorToDelete = _libraryRepository.GetAuthor(authorId);

            if (authorToDelete == null)
            {
                return NotFound();
            }

            _libraryRepository.DeleteAuthor(authorToDelete);

            if (!_libraryRepository.Save())
            {
                throw new Exception("Could not delete author");
            }

            return NoContent();
        }
    }
}
