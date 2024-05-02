using Domain;

namespace Application.Activities.Dtos
{
    internal static class DtoExtensions
    {
        public static IEnumerable<ActivityDto> ToEnumerableDto(this IEnumerable<Activity> activities)
        {
            var dtos = new List<ActivityDto>();

            foreach (var activity in activities)
            {
                dtos.Add((ActivityDto)activity);
            }

            return dtos;
        }
    }
}
