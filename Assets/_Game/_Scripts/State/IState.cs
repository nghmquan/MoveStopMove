public interface IState<T>
{
    void Enter(T t);
    void Excute(T t);
    void Exit(T t);
}
