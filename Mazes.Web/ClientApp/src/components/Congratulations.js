import { observer } from 'mobx-react-lite';

export const Congratulations = observer(({playerStore}) => {
    if (!playerStore.solved) {
      return;
    }

    const divStyle = {
        backgroundColor: 'rgba(0, 0, 0, 0.7)',
        display: 'flex',
        maxWidth: '600px',
        minHeight: '600px',
        marginTop: '6px',
        textAlign: 'center',
        top: '54%',
        left: '50%',
        transform: 'translate(-50%, -50%)',
        justifyContent: 'center',
        alignItems: 'center',
        position: 'fixed',
        zIndex: '1',
        right: '0',
        bottom: '0',
        opacity: '1',
        flexDirection: 'column',
        transition: 'visibility 0s, opacity 0.3s linear',
        color: 'rgba(250, 250, 250, 1)'
    }


    return (
        <>
        <div style={divStyle}>
            <h1>Congratulations!</h1>
                <p>You solved the maze!</p>
            </div>
        </>
    );
  });   