namespace MacketRouter.Utilities;

public static class ExceptionHelper
{
    public static void Throw(string text)
    {
        throw new Exception(text);
    }
}