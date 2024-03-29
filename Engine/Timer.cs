using System;

public class Timer
{
    private Action _callback;
    private float _duration;
    private bool _repeat;
    private float _time;
    private bool _active = false;

    public void Start(Action callback, float duration, bool repeat)
    {
        _callback = callback;
        _duration = duration;
        _repeat = repeat;
        _time = 0.0f;
        _active = true;
    }

    public void Update(float deltaTime)
    {
        if (!_active)
        {
            return;
        }

        _time = _time + deltaTime;

        if (_time > _duration)
        {
            _callback.Invoke();

            if (_repeat)
            {
                _time = 0.0f;
            }
            else
            {
                _active = false;
            }
        }
    }
}
