
namespace DbService.Models

    module User =
        open System

        type User =
            {
                Id: int
                Name: string
                Age: int
                CreatedAt: DateTimeOffset
            }

