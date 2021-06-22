import React, {useState} from "react";
import {Button, Input, Modal} from "@skbkontur/react-ui";
import "./SavingModal.css"


function SavingModal(props: any) {
  const [errorText, setErrorText] = useState("")
  const [isSaved, setIsSaved] = useState(false)
  const [name, setName] = useState("")

  return (
      <Modal onClose={() => props.onClose()}>
        <Modal.Header>Save your maze</Modal.Header>
        <Modal.Body>
          {isSaved ? <label className='content-item modal-header'>Saved</label>
              : <div className='modal-body-content'>

                <label className='content-item modal-header'>Enter the name of the maze</label>
                <label className='content-item subtitle'>{errorText}</label>
                <Input onChange={(event) => setName(event.target.value)}/>
              </div>}

        </Modal.Body>
        <Modal.Footer>
          {!isSaved && <Button use="pay" size="medium" onClick={() => {
            let prom: Promise<any> = props.saveMaze(name)
            prom.then((res) => {
                  if (res.ok)
                    setIsSaved(true)
                  else {
                    res.json().then((data: any) => {
                      setErrorText(data.errors.Name.join(". "))
                    }).catch((e: any) => console.log(e))
                  }

                }
            ).catch((e) => console.log(e))
          }}>Save</Button>}
        </Modal.Footer>
      </Modal>
  );
}

export default SavingModal