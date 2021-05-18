import React, {useState} from 'react';
import './App.css';
import Grid from './components/Grid/Grid'
import Header from './components/Header/Header'
import {CellData, Field, CellIndex} from './components/Extentions/Interfaces'
import {UNCLICKABLE_CELL_TYPES} from "./components/Extentions/Constants";

const rows = 30
const columns = 50


function App() {
  const [fieldSize, setFieldSize] = useState<{ rows: number, columns: number }>({rows: rows, columns: columns})
  const [field, setField] = useState<Field>(getEmptyField(fieldSize.rows, fieldSize.columns))

  function executeAlgorithm(name: string) {
    fetch("/algorithm/execute", {
      method: "POST",
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        name: name,
        start: `${field.start.x},${field.start.y}`,
        goal: `${field.end.x},${field.end.y}`,
        "allowDiagonal": 0,
        "metricName": 0,
        grid: parseCellsDataToNumbers(field.field)
      })
    })
        .then((res) => res.json()
            .then((data) => {
              let visitedPromises = getVisitedPrintPromises(data.states, field, setField)
              Promise.all(visitedPromises).then((res) => {
                printPath(data.resultPath, field, setField)
              })
            }))
        .catch((e) => console.log(e))
  }

  return (
      <div className="App">
        <Header field={field} clearFunc={() => setField(getEmptyField(fieldSize.rows, fieldSize.columns))}
                exec={executeAlgorithm}/>
        <Grid rows={fieldSize.rows} columns={fieldSize.columns} field={field.field}
              func={(x: number, y: number, data: CellData[]) => setField(getUpdatedField(x, y, data, field))}

        />
      </div>
  );
}


function printPath(pathData: string[], field: Field, setField: (f: Field) => void) {
  let indexes = getCellsIndexes(pathData.splice(1, pathData.length - 2))
  let newField = field.field.slice()
  for (let i = 0; i < indexes.length; i++) {
    let index = indexes[i]
    setTimeout(() =>{
      newField[index.x][index.y] = {...newField[index.x][index.y], state: 'path'}
      setField({
        ...field, field: newField
      })}, 100 * i)
  }

}

function getVisitedPrintPromises(states: any[], field: Field, setField: (f: Field) => void) {
  let visitedPromises = []
  for(let i = 0; i < states.length; i++){
    let pointData = states[i]
    let indexes = getCellIndex(pointData.point)
    let newField = field.field.slice()
    if (pointData.name === 'текущая вершина'
        || UNCLICKABLE_CELL_TYPES.includes(newField[indexes.x][indexes.y].state))
      continue

    visitedPromises.push( new Promise((resolve, reject) => setTimeout(() =>{
      newField[indexes.x][indexes.y] = {...newField[indexes.x][indexes.y], state: 'visited'}
      resolve(setField({
        ...field, field: newField
      }))}, 30 * i)))
  }
  return visitedPromises
}



function parseCellsDataToNumbers(data: CellData[][]) {
  return data.map((row) => {
    return row.map((cellData) => cellData.value)
  })
}

function getUpdatedField(x: number, y: number, data: CellData[], field: Field) {
  let newField = field.field.slice()
  for (let nodeData of data) {
    newField[x][y] = nodeData
  }
  return {
    ...field, field: newField
  }
}

function getCellsIndexes(data: string[]) {
  return data.map((str) => getCellIndex(str))
}

function getCellIndex(str: string) {
  let indexes = str.split(', ')
  return {x: parseInt(indexes[0]), y: parseInt(indexes[1])}
}

function clearPath(field: CellData[][]) {

}


function getEmptyField(rows: number, columns: number) {
  let result: CellData[][] = []
  for (let i = 0; i < rows; i++) {
    result.push(new Array(columns).fill({
      state: 'empty',
      value: 0
    }))
  }
  result[5][10] = {
    state: 'start',
    value: 0
  }
  result[10][20] = {
    state: 'end',
    value: 0
  }

  return {
    start: {x: 5, y: 10},
    end: {x: 10, y: 20},
    field: result
  }
}

export default App;