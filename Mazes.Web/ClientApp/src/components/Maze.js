import React, { Component } from 'react';

const MazeRenderer = ({ maze }) => {
    const renderCells = () => {
      const rows = [];
  
      for (let i = 0; i < maze.board.length; i++) {
        const cells = [];
  
        for (let j = 0; j < maze.board[i].length; j++) {
          const cell = maze.board[i][j];
          const cellStyle = {
            borderRight: cell.isRightActive ? '1px solid black' : '1px solid white',
            borderLeft: cell.isLeftActive ? '1px solid black' : '1px solid white',
            borderTop: cell.isUpperActive ? '1px solid black' : '1px solid white',
            borderBottom: cell.isLowerActive ? '1px solid black' : '1px solid white',
            width: '40px',
            height: '40px',
            display: 'inline-block',
          };
  
          cells.push(
            <div key={`${cell.X}-${cell.Y}`} style={cellStyle}></div>
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
  };

export class Maze extends Component {
  static displayName = Maze.name;

  constructor(props) {
    super(props);
    this.state = { loading: true, maze: null };
  }

  componentDidMount() {
    this.populateMazeData();
  }

  async populateMazeData() {
    const response = await fetch('maze');
    const data = await response.json();
    this.setState({ maze: data, loading: false });
  }

  static renderForecastsTable(forecasts) {
    return (
      <table className="table table-striped" aria-labelledby="tableLabel">
        <thead>
          <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map(forecast =>
            <tr key={forecast.date}>
              <td>{forecast.date}</td>
              <td>{forecast.temperatureC}</td>
              <td>{forecast.temperatureF}</td>
              <td>{forecast.summary}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : <MazeRenderer maze={this.state.maze} />;

    return (
      <div>
        <h1 id="tableLabel">Maze</h1>
        {contents}
      </div>
    );
  }
}
