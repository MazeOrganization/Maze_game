import React from 'react';
import { observer } from 'mobx-react-lite';
import parents from '../images/parents.png';
import kid from '../images/kid.png';

const MazeRenderer = observer(({ maze, userPosition }) => {
    const renderCells = () => {
      const rows = [];
  
      for (let i = 0; i < maze.board.length; i++) {
        const cells = [];
  
        for (let j = 0; j < maze.board[i].length; j++) {
          const cell = maze.board[i][j];
          const cellStyle = {
            borderRight: cell.isRightActive ? '2px solid white' : '0px solid transparent',
              borderLeft: cell.isLeftActive ? '2px solid white' : '0px solid transparent',
              borderTop: cell.isUpperActive ? '2px solid white' : '0px solid transparent',
              borderBottom: cell.isLowerActive ? '2px solid white' : '0px solid transparent',
            backgroundImage: cell.x === maze.board.length - 1 && cell.y === maze.board.length - 1 ? `url(${kid})` : 'none',
            backgroundSize: 'contain',
            backgroundColor: 'rgba(0, 0, 0, 0.5)',
            width: '30px',
            height: '30px',
            display: 'inline-block',
            position: 'relative'
          };
          
            const playerStyle = {
                backgroundImage: `url(${parents})`,
                backgroundSize: 'contain',
                backgroundRepeat: 'no-repeat',
                position: 'absolute',
                top: '54%',
                left: '50%',
                transform: 'translate(-50%, -50%)',
                width: '120%',
                height: '120%',
            }
  
          cells.push(
            <div key={`${cell.x}-${cell.y}`} style={cellStyle}>
              {userPosition[0] === cell.x && userPosition[1] === cell.y && <div style={playerStyle}></div>}
            </div>
          );
        }
  
        rows.push(
          <div key={`row-${i}`} style={{ display: 'flex' }}>
            {cells}
          </div>
        );
      }
  
      return rows;
    };
  
    return (
      <div>
        {renderCells()}
      </div>
    );
  });

export const Maze = observer((props) => {
  let contents = !props.appStore.mazeStore.maze
      ? <p><em>Loading...</em></p>
      : <MazeRenderer
          userPosition={props.appStore.playerStore.userPosition}
          maze={props.appStore.mazeStore.maze}
        />;

  return (
      <div style={{ width: '600px', margin: '0 auto' }}>
      {contents}
    </div>
  );
});
