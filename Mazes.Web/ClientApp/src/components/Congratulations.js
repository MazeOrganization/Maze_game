import { observer } from 'mobx-react-lite';

export const Congratulations = observer(({playerStore}) => {
    if (!playerStore.solved) {
      return;
    }
  
    return (
      <div style={{float: 'right'}}>
        <h1>Congratulations!</h1>
        <p style={{color: 'green'}}>You solved the maze!</p>
      </div>
    );
  });   