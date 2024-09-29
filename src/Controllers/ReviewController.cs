using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Entity;

namespace src.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private static List<Review> reviews = new List<Review>();

        [HttpGet]
        public ActionResult<Review> GetReviews()
        {
            return Ok(reviews);
        }

        [HttpGet("{reviewid}")]
        public ActionResult GetReview(Guid reviewid)
        {
            var review = reviews.FirstOrDefault(r => r.ReviewId == reviewid);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        [HttpPost]
        public ActionResult AddReview(Review newReview)
        {
            reviews.Add(newReview);
            return CreatedAtAction(nameof(GetReview), new { reviewid = newReview.ReviewId }, newReview);
        }

        [HttpDelete("{reviewid}")]
        public ActionResult DeleteReview(Guid reviewid)
        {
            var review = reviews.FirstOrDefault(r => r.ReviewId == reviewid);
            if (review == null)
            {
                return NotFound();
            }
            reviews.Remove(review);
            return NoContent();
        }

        [HttpPut("{reviewid}")]
        public ActionResult UpdateReview(Guid reviewid, Review updatedReview)
        {
            var review = reviews.FirstOrDefault(r => r.ReviewId == reviewid);
            if (review == null)
            {
                return NotFound();
            }
            review.Comment = updatedReview.Comment;
            review.Rating = updatedReview.Rating;
            return Ok(review);
        }
    }
}