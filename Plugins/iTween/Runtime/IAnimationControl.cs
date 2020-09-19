
public interface IAnimationControl
{
	void Play();
	void Stop();
	void Pause();
	void Seek(double value);
	bool IsPlaying { get; }
}
