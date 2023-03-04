using AutoMapper;
using TatterFitness.Models.Exercises;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.Mapping
{
    public class ModelMapping : Profile
    {
        public ModelMapping()
        {
            CreateMap<WorkoutExerciseModifier, ExerciseModifier>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.ExerciseModifierId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.ModifierName));

            CreateMap<ExerciseModifier, WorkoutExerciseModifier>()
                .ForMember(dest => dest.ModifierName, opt => opt.MapFrom(source => source.Name))
                .ForMember(dest => dest.ModifierSequence, opt => opt.MapFrom(source => source.Sequence))
                .ForMember(dest => dest.ExerciseModifierId, opt => opt.MapFrom(source => source.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<WorkoutExerciseSet, WorkoutExerciseSet>();
            CreateMap<WorkoutExercise, WorkoutExercise>();
        }
    }
}
