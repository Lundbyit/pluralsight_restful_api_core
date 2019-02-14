using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pluralsight_restful_api_core.Entities;
using pluralsight_restful_api_core.Helpers;
using pluralsight_restful_api_core.Models;
using pluralsight_restful_api_core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace pluralsight_restful_api_core.Controllers
{
    [Route("api/authorcollections")]
    public class AuthorCollectionsController : Controller
    {
        private ILibraryRepository _libraryRepository;

        public AuthorCollectionsController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpPost()]
        public IActionResult CreateAuthorCollection([FromBody] ICollection<AuthorForCreationDto> authorCollection)
        {
            if (authorCollection == null)
            {
                return BadRequest();
            }

            var authorEntities = Mapper.Map<IEnumerable<Author>>(authorCollection);

            foreach (var author in authorEntities)
            {
                _libraryRepository.AddAuthor(author);
            }

            if (!_libraryRepository.Save())
            {
                throw new Exception("Failed to create authors in collection");
            }

            var authorCollectionToReturn = Mapper.Map<IEnumerable<AuthorDto>>(authorEntities);
            var authorIdsAsString = string.Join(",", authorCollectionToReturn.Select(a => a.Id));

            return CreatedAtRoute("GetAuthorCollection",
                new { authorIds = authorIdsAsString },
                authorCollectionToReturn);
        }

        [HttpGet("({authorIds})", Name = "GetAuthorCollection")]
        public IActionResult GetAuthorCollection(
            [ModelBinder(typeof(ArrayModelBinder))] IEnumerable<Guid> authorIds)
        {
            if (authorIds == null)
            {
                return BadRequest();
            }

            var authorEntities = _libraryRepository.GetAuthors(authorIds);

            if (authorIds.Count() != authorEntities.Count())
            {
                return NotFound();
            }

            var authorsToReturn = Mapper.Map<IEnumerable<AuthorDto>>(authorEntities);
            return Ok(authorsToReturn);
        }
    }
}
