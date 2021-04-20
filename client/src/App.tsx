import React, { useState } from 'react';
import logo from './logo.svg';
import './App.css';
import Grid from './components/Grid/Grid'
import Header from './components/Header/Header'
import {Button} from '@skbkontur/react-ui'

const rows = 30
const columns = 60

function App() {
  const [fieldSize, setFieldSize] = useState<{rows: number, columns: number}>({rows: rows, columns: columns})
  const [field, setField] = useState<boolean[][]>(getEmptyField(fieldSize.rows, fieldSize.columns))

  function updateField(x: number, y: number, value: boolean) {
    let newField = field.slice()
    newField[x][y] = value
    setField(newField)
  }

  return (
    <div className="App">
      <Header field={field} clearFunc={() =>setField(getEmptyField(fieldSize.rows, fieldSize.columns))} />
      {/* <Button className="my-elem" 
      onClick={() =>{setField(() => getEmptyField(fieldSize.rows, fieldSize.columns))}}> Pause</Button> */}
      <Grid rows={fieldSize.rows} columns={fieldSize.columns} field={field} func={updateField} />
    </div>
  );
}

interface HeaderProps{
  
}

function getEmptyField(rows: number, columns: number){
  let result: boolean[][] = []
  for (let i = 0; i < rows; i++){
      let t = new Array(columns).fill(false)
      result.push(t)             
  }
  return result
}

export default App;
