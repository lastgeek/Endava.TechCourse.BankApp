namespace Endava.TechCourse.BankApp.Test.Common
{
    public class ApplicationDataAttribute : InlineAutoDataAttribute
    {
        public ApplicationDataAttribute(params object[] arguments)
            : base(() =>
            {
                var fixture = new Fixture()
                    .Customize(new CompositeCustomization(
                        new AutoNSubstituteCustomization(),
                        new SqliteCustomization
                        {
                            AutoOpenConnection = true,
                            OmitDbSets = true,
                            OnCreate = OnCreateAction.EnsureCreated
                        }));

                ((IFixture)fixture).Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => ((IFixture)fixture).Behaviors.Remove(b));

                ((IFixture)fixture).Behaviors.Add(new OmitOnRecursionBehavior());

                return fixture;
            }, arguments)
        {
        }
    }
}