import React, {useState} from 'react';
import './App.css';
import Grid from './components/Grid/Grid'
import Header from './components/Header/Header'
import {CellData, Field, CellIndex} from './components/Extentions/Interfaces'
import {UNCLICKABLE_CELL_TYPES} from "./components/Extentions/Constants";
import {parseCellsDataToNumbers} from "./components/Extentions/Functions";


function App() {
  const [fieldSize, setFieldSize] = useState<{ rows: number, columns: number }>({rows: 0, columns: 0})
  const [field, setField] = useState<Field>({start:{x:0 ,y: 0}, end: {x:0 ,y: 0}, field: [] })

  function initField(rows: number, columns: number){
    setFieldSize({rows: rows, columns: columns})
    setField(getEmptyField(rows, columns))
  }


  function executeAlgorithm(name: string) {
    let a =  parseCellsDataToNumbers(field.field)
    console.log(a)
    fetch("/algorithm/execute", {
      method: "POST",
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        name: name,
        start: `${field.start.x},${field.start.y}`,
        goal: `${field.end.x},${field.end.y}`,
        "allowDiagonal": true,
        "metricName": 0,
        grid: a
      })
    })
        .then((res) => res.json()
            .then((data) => {
              console.log(data)
              setField(getFieldWithoutExecuteVisualize(field))
              let visitedPromises = getVisitedPrintPromises(data.renderedStates, field, setField)
              Promise.all(visitedPromises).then((res) => {
                printPath(data.result.path, field, setField, data.result.color)
              })
            }))
        .catch((e) => console.log(e))
  }


  return (
      <div className="App">
        <Header initField={initField}
                clearField={() => setField(getEmptyField(fieldSize.rows, fieldSize.columns))}
                executeAlgorithm={executeAlgorithm} clearPath={() => setField(getFieldWithoutExecuteVisualize(field))}
                setPrebuildField={(newField: CellData[][]) => setField({...field, field: newField})}
                saveMaze={(name: string) => saveMaze(field.field, name)}/>

        <Grid rows={fieldSize.rows} columns={fieldSize.columns} field={field.field}
              func={(x: number, y: number, data: CellData) => setField(getUpdatedField(x, y, data, field))}
        />
      </div>
  );
}


function printPath(pathData: string[], field: Field, setField: (f: Field) => void, color: string) {
  let indexes = getCellsIndexes(pathData.splice(1, pathData.length - 2))
  let newField = field.field.slice()
  for (let i = 0; i < indexes.length; i++) {
    let index = indexes[i]
    setTimeout(() =>{
      newField[index.x][index.y] = {...newField[index.x][index.y], state: 'path', mainColor: color}
      setField({
        ...field, field: newField
      })}, 1 * i)
  }

}

function getVisitedPrintPromises(states: any[], field: Field, setField: (f: Field) => void) {
  let visitedPromises = []
  for(let i = 0; i < states.length - 1; i++){
    let pointData = states[i]
    let indexes = getCellIndex(pointData.renderedPoint)
    let newField = field.field.slice()
    if (UNCLICKABLE_CELL_TYPES.includes(newField[indexes.x][indexes.y].state))
      continue

    visitedPromises.push( new Promise((resolve, reject) => setTimeout(() =>{
      newField[indexes.x][indexes.y] = {...newField[indexes.x][indexes.y],
        state: 'visited',
        mainColor: pointData.color }
      resolve(setField({
        ...field, field: newField
      }))}, 60 * i)))
  }
  return visitedPromises
}


function getFieldWithoutExecuteVisualize(field: Field) {
  let newField = field.field.slice()
  for (let i = 0; i < newField.length; i++)
    for (let j = 0; j < newField[i].length; j++){
      let state = newField[i][j].state
      if (state === 'visited' || state === 'path')
        newField[i][j] = {...newField[i][j], state: 'empty', mainColor: 'white'}
    }
  return {
      ...field,
      field: newField
  }
}


function saveMaze(field: CellData[][], name: string) {
  fetch('/maze', {
    method: 'PUT', // Method itself
    headers: {
      'Content-type': 'application/json; charset=UTF-8' // Indicates the content
    },
    body: JSON.stringify({
      name: name,
      grid: parseCellsDataToNumbers(field)
    }) // We send data in JSON format
  }).catch((e) => console.log(e))



  // fetch("/maze", {
  //   method: "PUT",
  //   headers: {
  //     'Content-Type': 'application/json-patch+json',
  //   },
  //   body: JSON.stringify({
  //     name: name,
  //     grid: parseCellsDataToNumbers(field)
  //   })
  // })
}

function getUpdatedField(x: number, y: number, data: CellData, field: Field) {
  let newField = field.field.slice()
  newField[x][y] = data
  let index = {
    x: x,
    y: y
  }
  if (data.state === 'start')
    return {
      ...field,
      start: index,
      field: newField
    }
  if (data.state === 'end')
    return {
      ...field,
      end: index,
      field: newField
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



function getEmptyField(rows: number, columns: number) {
  let result: CellData[][] = []
  for (let i = 0; i < rows; i++) {
    result.push(new Array(columns).fill({
      state: 'empty',
      value: 1,
      mainColor: "white"
    }))
  }
  result[5][10] = {
    state: 'start',
    value: 1,
    mainColor: 'red'
  }
  result[10][20] = {
    state: 'end',
    value: 1,
    mainColor: "#19c43c"
  }

  return {
    start: {x: 5, y: 10},
    end: {x: 10, y: 20},
    field: result
  }
}

export default App;