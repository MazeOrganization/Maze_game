import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';

const MazeRenderer = observer(({ maze, userPosition }) => {
    const renderCells = () => {
      const rows = [];
  
      for (let i = 0; i < maze.board.length; i++) {
        const cells = [];
  
        for (let j = 0; j < maze.board[i].length; j++) {
          const cell = maze.board[i][j];
          const cellStyle = {
            borderRight: cell.isRightActive ? '2px solid black' : '0px solid white',
            borderLeft: cell.isLeftActive ? '2px solid black' : '0px solid white',
            borderTop: cell.isUpperActive ? '2px solid black' : '0px solid white',
            borderBottom: cell.isLowerActive ? '2px solid black' : '0px solid white',
            backgroundColor: cell.x === 0 & cell.y === 0
              ? 'green'
              : cell.x === maze.board.length - 1 && cell.y === maze.board.length - 1
                ? 'red'
                : 'white',
            width: '30px',
            height: '30px',
            display: 'inline-block',
            position: 'relative'
          };
          
          const playerStyle = {
            backgroundColor: 'blue',
            borderRadius: '50%',
            position: 'absolute',
            top: '50%',
            left: '50%',
            transform: 'translate(-50%, -50%)',
            width: '50%',
            height: '50%',
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
        <h2>Maze ID: {maze.id}</h2>
        {renderCells()}
      </div>
    );
  });

export const Maze = observer((props) => {
  useEffect(() => {
    props.appStore.mazeStore.fetchMaze();
  }, []);

  let contents = !props.appStore.mazeStore.maze
      ? <p><em>Loading...</em></p>
      : <MazeRenderer
          userPosition={props.appStore.playerStore.userPosition}
          maze={props.appStore.mazeStore.maze}
        />;

  return (
    <div>
      <h1 id="tableLabel">Maze</h1>
      {contents}
    </div>
  );
});
