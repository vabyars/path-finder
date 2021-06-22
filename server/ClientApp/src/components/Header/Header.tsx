import React, {useEffect, useState} from 'react'
import {Button, Input, Modal, Radio} from '@skbkontur/react-ui'
import './Header.css'
import Select from 'react-select';
import {CellData, HeaderProps, SelectData} from "../Extentions/Interfaces";
import {getCellIndex, parseCellsDataToNumbers} from "../Extentions/Functions";
import SavingModal from "../SavingModal/SavingModal";


function parseArrayToSelectData(data: any[]) {
  return data.map(function (label, i) {
    return {label: label, value: i + 1}
  })
}

function Header(props: HeaderProps) {
  const [algorithms, setAlgorithms] = useState<SelectData[]>([])
  const [currentAlgorithm, setAlgorithm] = useState<SelectData>()
  const [mazes, setMazes] = useState<SelectData[]>([])
  const [currentMaze, setMaze] = useState<SelectData>()
  const [isModalOpen, setIsModalOpen] = useState(false)
  const [allowDiagonal, setAllowDiagonal] = useState(false)
  const [metrics, setMetrics] = useState<SelectData[]>([])
  const [currentMetric, setCurrentMetrics] = useState<SelectData>()

  async function loadMazesAndAlgorithms() {
    let data = await (await fetch("/settings")).json()
    console.log(data)
    let parsedAlgorithms = parseArrayToSelectData(data.algorithms)
    let parsedMazes = parseArrayToSelectData(data.mazes)
    let parsedMetrics = parseArrayToSelectData(data.metrics)
    props.initField(data.height, data.width)

    setMazes(parsedMazes)
    setAlgorithms(parsedAlgorithms)
    setMetrics(parsedMetrics)
    setCurrentMetrics(parsedMetrics[0])
    setAlgorithm(parsedAlgorithms[0])
    setMaze(parsedMazes[0])
  }

  useEffect(() => {
    loadMazesAndAlgorithms()
  }, [])

  function parseMazeFromServer(data: any) {
    let maze = data.maze
    let field: CellData[][] = []
    for (let i = 0; i < maze.length; i++) {
      for (let j = 0; j < maze[i].length; j++) {
        if (!field[i])
          field[i] = []
        field[i][j] = {
          value: maze[i][j],
          state: maze[i][j] === -1 ? 'wall' : 'empty',
          mainColor: maze[i][j] === -1 ? "black" : "white"
        }
      }
    }
    let start = getCellIndex(data.start)
    let end = getCellIndex(data.end)
    field[start.x][start.y] = {state: "start", mainColor: "red", value: 1}
    field[end.x][end.y] = {state: "end", mainColor: "#19c43c", value: 1}

    return {field: field,
      start: start,
      end: end}
  }

  return (
      <div className="header">
        <label className="flex-elem logo">Pathfinder</label>
        <Button className="flex-elem" onClick={() => {
          if (currentAlgorithm && currentMetric)
            props.executeAlgorithm(currentAlgorithm.label, currentMetric.label, allowDiagonal)
        }}> Start</Button>
        <Button className="flex-elem" onClick={props.clearField}> Clear field</Button>
        <Button className="flex-elem" onClick={props.clearPath}> Clear path</Button>

        {currentAlgorithm && <Select className="flex-elem algorithm" isSearchable={false}
                                     options={algorithms} defaultValue={currentAlgorithm}
                                     onChange={(value: any) => setAlgorithm(value)}/>}
        {currentMaze && <Select className="flex-elem mazes" isSearchable={true}
                                options={mazes} defaultValue={currentMaze} onChange={(value: any) => {
          setMaze(value)
          fetch(`/maze/${value.label}`)
              .then((res) => res.json()
                  .then((data) => props.setPrebuildField(parseMazeFromServer(data)))
                  .catch((e) => console.log(e)))
              .catch((e) => console.log(e))
        }}/>}
        <Radio value={allowDiagonal} checked={allowDiagonal}  onClick={() => setAllowDiagonal(!allowDiagonal)}/>
        <label style={{marginLeft: '5px'}} onClick={() => setAllowDiagonal(!allowDiagonal)}>Allow diagonal</label>

        {currentMetric && <Select className="flex-elem metrics" isSearchable={false}
                                     options={metrics} defaultValue={currentMetric}
                                     onChange={(value: any) => setCurrentMetrics(value)}/>}

        <Button className="flex-elem" onClick={() =>setIsModalOpen(true)}> Save maze</Button>
        {isModalOpen && <SavingModal saveMaze={props.saveMaze} onClose={setIsModalOpen}/>}
      </div>
  )
}


export default Header