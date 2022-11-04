import { Box, Button, Typography } from '@mui/material';
import Modal from '@mui/material/Modal';
import { modalBaseStyle } from '@styles/globalStyles';

export function ProfileModal({ open, handleClose }: any) {
  return (
    <Box>
      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={{ ...modalBaseStyle, justifyContent: 'space-between', minHeight: '300px' }} component="form">
          <Typography variant="h4">Upload picture</Typography>

          <Button onClick={() => handleClose()} variant="contained">
            Upload
          </Button>
        </Box>
      </Modal>
    </Box>
  );
}
