import React, {useState} from 'react';
import './App.css';
import Grid from './components/Grid/Grid'
import Header from './components/Header/Header'
import {CellData, Field} from './components/Extentions/Interfaces'
import {UNCLICKABLE_CELL_TYPES} from "./components/Extentions/Constants";
import {getCellIndex, parseCellsDataToNumbers} from "./components/Extentions/Functions";


const timeouts: any = []

function clearAllTimeout() {
  while (timeouts.length > 0){
    let t = timeouts.shift()
    clearTimeout(t)
  }
}


function App() {
  const [fieldSize, setFieldSize] = useState<{ rows: number, columns: number }>({rows: 0, columns: 0})
  const [field, setField] = useState<Field>({start:{x:0 ,y: 0}, end: {x:0 ,y: 0}, field: [] })

  function initField(rows: number, columns: number){
    setFieldSize({rows: rows, columns: columns})
    setField(getEmptyField(rows, columns))
  }


  function executeAlgorithm(name: string, metricName: string, allowDiagonal: boolean) {
    clearAllTimeout()
    let body =  JSON.stringify({
      name: name,
      start: `${field.start.x},${field.start.y}`,
      goal: `${field.end.x},${field.end.y}`,
      "allowDiagonal": allowDiagonal,
      "metricName": metricName,
      grid: parseCellsDataToNumbers(field.field)
    })
    console.log(body)
    fetch("/algorithm/execute", {
      method: "POST",
      headers: {
        'Content-Type': 'application/json',
      },
      body: body
    })
        .then((res) => res.json()
            .then((data) => {
              setField(getFieldWithoutExecuteVisualize(field))
              let visitedPromises = getVisitedPrintPromises(data.renderedStates, field, setField)
              printPath(data.result.path, field, setField, data.result.color, visitedPromises)
            }))
        .catch((e) => console.log(e))
  }


  return (
      <div className="App">
        <Header initField={initField}
                clearField={() => {
                  clearAllTimeout()
                  setField(getEmptyField(fieldSize.rows, fieldSize.columns))
                }}
                executeAlgorithm={executeAlgorithm} clearPath={() =>{
                  clearAllTimeout()
                  setField(getFieldWithoutExecuteVisualize(field))}
                  }
                setPrebuildField={(newField: Field) => setField(newField)}
                saveMaze={(name: string) => saveMaze(field, name)
                }/>

        <Grid rows={fieldSize.rows} columns={fieldSize.columns} field={field.field}
              func={(x: number, y: number, data: CellData) => setField(getUpdatedField(x, y, data, field))}
              printStartAndEnd={() => {
                let newField = field.field.slice()

                for (let i = 0; i < field.field.length; i++) {
                  for (let j = 0; j < field.field[i].length; j++) {
                    if ((field.field[i][j].state ==='start' || field.field[i][j].state ==='end')
                        && ((i === field.start.x || i === field.end.x) && (j === field.start.y || j === field.end.y)))
                      newField[i][j] = {state: "empty", mainColor: "white", value: 1 }
                  }
                }
                newField[field.start.x][field.start.y] = {state: "start", mainColor: "red", value: 1}
                newField[field.end.x][field.end.y] = {state: "end", mainColor: "#19c43c", value: 1}
                setField({...field, field: newField})
              }}
        />
      </div>
  );
}




function printPath(pathData: string[], field: Field, setField: (f: Field) => void, color: string, delay: number) {
  let indexes = getCellsIndexes(pathData.splice(1, pathData.length - 2))
  let newField = field.field.slice()
  for (let i = 0; i < indexes.length; i++) {
    let index = indexes[i]
    timeouts.push(setTimeout(() => {
      newField[index.x][index.y] = {...newField[index.x][index.y], state: 'path', mainColor: color}
      setField({
        ...field, field: newField
      })}, delay +  50 * i))
  }
}

function getVisitedPrintPromises(states: any[], field: Field, setField: (f: Field) => void) {
  let delay = 10
  for(let i = 0; i < states.length - 1; i++){
    let pointData = states[i]
    let indexes = getCellIndex(pointData.renderedPoint)
    let newField = field.field.slice()
    if (UNCLICKABLE_CELL_TYPES.includes(newField[indexes.x][indexes.y].state))
      continue

    timeouts.push(setTimeout(() =>{
      newField[indexes.x][indexes.y] = {...newField[indexes.x][indexes.y],
        state: 'visited',
        mainColor: pointData.color }
      setField({
        ...field, field: newField
      })}, delay * i))

    timeouts.push(setTimeout(() =>{
      newField[indexes.x][indexes.y] = {...newField[indexes.x][indexes.y],
        state: 'visited',
        mainColor: pointData.secondColor }
      setField({
        ...field, field: newField
      })}, delay * 1.25 * i))
  }
  return delay * 1.25 * (states.length - 1)
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

function saveMaze(field: Field, name: string) {
  return  fetch('/maze', {
    method: 'PUT', // Method itself
    headers: {
      'Content-type': 'application/json' // Indicates the content
    },
    body: JSON.stringify({
      name: name,
      grid: parseCellsDataToNumbers(field.field),
      start: `${field.start.x},${field.start.y}`,
      end: `${field.end.x},${field.end.y}`,
    })
  })
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