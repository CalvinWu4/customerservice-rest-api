using System;
using System.Collections.Generic;
using System.Linq;
using CustomerServiceRESTAPI.Entities;
using CustomerServiceRESTAPI.Models;

namespace CustomerServiceRESTAPI.Services
{
    public class ReviewRepository : IReviewRepository
    {
        private Context _context;

        public ReviewRepository(Context context)
        {
            _context = context;
        }

        public void AddReview(Review review)
        {
            _context.Add(review);
        }

        public Review GetReview(int Id)
        {
            return _context.Reviews.FirstOrDefault(t => t.Id == Id);
        }

        public IEnumerable<Review> GetReviews()
        {
            return _context.Reviews.OrderBy(t => t.DateCreated).ToList();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void DeleteReview(Review review)
        {
            _context.Remove(review);
        }

        public void UpdateReview(Review review)
        {
            _context.Update(review);
        }
    }
}
