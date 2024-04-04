//using Infrastructure.Entities;
//using Infrastructure.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Infrastructure.Factories;

//public class CourseFactory
//{
//    public static CourseModel Create(CourseEntity entity)
//    {
//        try
//        {
//            return new CourseModel
//            {
//                Id = entity.Id,
//                Title = entity.Title,
//                Price = entity.Price,
//                DiscountPrice = entity.DiscountPrice,
//                EstimatedHours = entity.EstimatedHours,
//                IsBestSeller = entity.IsBestSeller,
//                UserVotes = entity.UserVotes,
//                LikeParameter = entity.LikeParameter,
//                Category = entity.Category,
//                Creator = entity.Creator,
//            };
//        }
//        catch (Exception ex) { }
//        }

//    public static CourseEntity Create(CourseCreateDto dto)
//    {
//        try
//        {
//            return new CourseEntity
//            {
//                    Title = dto.Title,
//                    Price = dto.Price,
//                    DiscountPrice = dto.DiscountPrice,
//                    EstimatedHours = dto.EstimatedHours,
//                    IsBestSeller = dto.IsBestSeller,
//                    IsDigital = dto.IsDigital,
//                    UserVotes = dto.UserVotes,
//                    LikeParameter = dto.LikeParameter,
//                    Category = dto.Category,
//                    Creator = dto.Creator,
//                    ImageName = dto.ImageName,
//                };
//        }
            
//         }
//        catch (Exception ex) { }
//        }

//    }
