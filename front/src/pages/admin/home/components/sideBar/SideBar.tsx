import { Box, List, ListItem, ListItemButton, ListItemText, ListItemIcon } from '@mui/material';
import { useState } from 'react';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';

export function SideBar({ items, openAddProductModal }: any) {
  const [selectedCategory, setSelectedCategory] = useState(0);

  return (
    <Box sx={{ width: '100%', maxWidth: 360 }}>
      <nav aria-label="main mailbox folders">
        <List sx={{ padding: 0 }}>
          {items.map((item: any, i: number) => (
            <ListItem disablePadding selected={selectedCategory === i} divider={i !== items.length - 1} key={item.name}>
              <ListItemButton onClick={() => setSelectedCategory(i)}>
                <ListItemText inset primary={item.name} />
              </ListItemButton>
            </ListItem>
          ))}
          <ListItem disablePadding sx={{ backgroundColor: 'lightBlue' }}>
            <ListItemButton onClick={openAddProductModal}>
              <ListItemIcon>
                <AddCircleOutlineIcon />
              </ListItemIcon>
              <ListItemText primary="Add new item" />
            </ListItemButton>
          </ListItem>
        </List>
      </nav>
    </Box>
  );
}
