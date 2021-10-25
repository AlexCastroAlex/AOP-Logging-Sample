using AOP_Logging_PostSharp_Sample.Common.Aspects;
using AOP_Logging_PostSharp_Sample.Common.Models;

namespace AOP_Logging_PostSharp_Sample.Repositories
{
    [Log]
    public class ArticleRepository :IArticleRepository
    {
        private static readonly IEnumerable<Article> Articles = new List<Article>
        {
            new Article
            {
                Id = 1,
                Author = "Alex CASTRO",
                Name = "AOP using PostSharp",
                PublishDate = new DateTime(2020, 10, 29)
            },
            new Article
            {
                Id = 2,
                Author = "Martin Fowler",
                Name = "Clean Code",
                PublishDate = new DateTime(2020, 1, 13)
            },
            new Article
            {
                Id = 3,
                Author = "Gang of Four",
                Name = "Design patterns",
                PublishDate = new DateTime(2020, 6, 30)
            },
            new Article
            {
                Id = 4,
                Author = "C. Mommer",
                Name = "Docker pour les développeurs .Net",
                PublishDate = new DateTime(2020, 7, 30)
            },
            new Article
            {
                Id = 5,
                Author = "C. Mommer",
                Name = "Blazor :développement front end d'application web dynamiques",
                PublishDate = new DateTime(2020, 8, 13)
            }
        };

        public  IEnumerable<Article> GetArticles()
        {
            return  Articles.OrderByDescending(article => article.PublishDate);
        }

        public Article GetArticle(int id)
        {
            return Articles.Single(article => article.Id == id);
        }

        public Article AddArticle(Article article)
        {
            article.Id = Articles.Max(c => c.Id)+1;
            Articles.ToList().Add(article);
            return article;
        }

    }
}
