using AOP_Logging_PostSharp_Sample.Common.Aspects;
using AOP_Logging_PostSharp_Sample.Common.Models;
using AOP_Logging_PostSharp_Sample.Repositories;

namespace AOP_Logging_PostSharp_Sample.Services
{
    [Log]
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _repository;

        public ArticleService(IArticleRepository repository)
        {
            _repository = repository;
        }

        public  IEnumerable<Article> GetArticles()
        {
            return  _repository.GetArticles();
        }

        public Article GetArticle(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("The Id should be greater than 0", nameof(id));
            }

            return _repository.GetArticle(id);
        }

        public Article AddArticle(Article article)
        {
            if(article.Id <= 0)
            {
                throw new ArgumentException($"invalid id parameter for {typeof(Article)}");
            }
            else if(_repository.GetArticles().Any(c=>c.Id ==article.Id))
            {
                throw new ArgumentException($"already existing article {typeof(Article)}");
            }

            var articleAdded = _repository.AddArticle(article);
            return articleAdded;
        }

    }
}
