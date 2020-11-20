
namespace DbService.Mappers
    module User =
        
        let mapDbRepoToDbService (m: DbRepository.Models.User): DbService.Models.User.User =
            {
                Id = m.ID
                Name = m.Name
                Age = m.Age
                CreatedAt = m.CreatedAt
            }

