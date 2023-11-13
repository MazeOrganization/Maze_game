import { observer } from 'mobx-react-lite';

export const Time = observer(({playerStore}) => {
    const displayTime = () => {
      let milliseconds = playerStore.time;
      let seconds = Math.floor(milliseconds / 1000);
      milliseconds -= seconds * 1000;
      let minutes = Math.floor(seconds / 60);
      seconds -= minutes * 60;
      return `${minutes}:${seconds}.${milliseconds}`;
    }
    
    return (
        <div style={{ width: '100px', margin: '30px auto 0 auto' }}>
            <p style={{ color: 'white' }}>Time: {displayTime()}</p>
      </div>
    );
  });   