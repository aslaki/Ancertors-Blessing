using System;

public class Timer
{
    private float _timer;
    private float _duration;

    public bool HasElapsed => _timer >= _duration;

    public float CurrentTime => _timer;

    public float TimeLeft => _duration - _timer;

    public void Start(float duration)
    {
        _duration = duration;
    }

    public void Update(float deltaTime)
    {
        _timer += deltaTime;
    }
}