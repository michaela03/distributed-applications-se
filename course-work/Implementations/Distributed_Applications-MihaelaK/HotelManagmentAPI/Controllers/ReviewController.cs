using HotelManagmentAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HotelManagmentAPI.Dto;
using AutoMapper;
using System.Collections.Generic;
using HotelManagmentAPI.Repository;

namespace HotelManagmentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());
            return Ok(reviews);
        }

        [HttpGet("{reviewID}")]
        [ProducesResponseType(200, Type = typeof(ReviewDto))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewID)
        {
            if (!_reviewRepository.ReviewExists(reviewID))
                return NotFound();

            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewID));
            return Ok(review);
        }

        [HttpGet("client/{clientID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsByClient(int clientID)
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsByClients(clientID));
            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromBody] ReviewDto reviewDto)
        {
            if (reviewDto == null)
                return BadRequest();

            //if (_reviewRepository.GetReviewsByClients(reviewDto.ClientID) != null)
            //{
            //    ModelState.AddModelError("", "Review already exists");
            //    return StatusCode(422, ModelState);
            //}

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var review = _mapper.Map<Review>(reviewDto);

            if (!_reviewRepository.CreateReview(review))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            var createdDto = _mapper.Map<ReviewDto>(review);
            return CreatedAtAction(nameof(GetReviews), new { reviewDto = createdDto.ReviewID }, createdDto);
        }

        [HttpPut("{reviewID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(int reviewID, [FromBody] ReviewDto updatedReview)
        {
            if (updatedReview == null)
                return BadRequest(ModelState);

            if (reviewID != updatedReview.ReviewID)
                return BadRequest(ModelState);

            if (!_reviewRepository.ReviewExists(reviewID))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(updatedReview);

            if (!_reviewRepository.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating the review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{clientID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int reviewID)
        {
            if (!_reviewRepository.ReviewExists(reviewID))
                return NotFound();

            var reviewToDelete = _reviewRepository.GetReview(reviewID);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReview(reviewID))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the client");
                return StatusCode(500, ModelState);
            }

            return NoContent();




        }
    }
}
