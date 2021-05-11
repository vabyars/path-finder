import React, { useState } from 'react';
import './App.css';
import Grid from './components/Grid/Grid'
import Header from './components/Header/Header'

const rows = 30
const columns = 60



function App() {
  const [fieldSize, setFieldSize] = useState<{rows: number, columns: number}>({rows: rows, columns: columns})
  const [field, setField] = useState<boolean[][]>(getEmptyField(fieldSize.rows, fieldSize.columns))



  return (
    <div className="App">
      <Header  field={field} clearFunc={() =>setField(getEmptyField(fieldSize.rows, fieldSize.columns))} />
      <Grid rows={fieldSize.rows} columns={fieldSize.columns} field={field}
            func={(x: number, y: number, value: boolean) => setField(getUpdatedField(x, y, value, field))} />
    </div>
  );
}


function getUpdatedField(x: number, y: number, value: boolean, field: boolean[][]) {
  let newField = field.slice()
  newField[x][y] = value
  return newField
}


function getEmptyField(rows: number, columns: number){
  let result: boolean[][] = []
  for (let i = 0; i < rows; i++){
      result.push(new Array(columns).fill(false))
  }
  return result
}

export default App;